using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;

public class EnemyMovementSystem : JobComponentSystem 
{
	//由一系列组件组成
	private ComponentGroup enemyGroup;
	private ComponentGroup playerGroup;

	//系统创建时调用
	protected override void OnCreateManager()
	{
		//声明该组所需的组件
		enemyGroup = GetComponentGroup
		(
			ComponentType.ReadOnly(typeof(VelocityComponent)),
			ComponentType.ReadOnly(typeof(EnemyComponent)),
			typeof(Position)
		);
		playerGroup = GetComponentGroup
		(
			ComponentType.ReadOnly(typeof(InputComponent)),
			ComponentType.ReadOnly(typeof(Position))
		);
	}

	[BurstCompile]//使用Burst编译
	struct EnemyMoveJob : IJobParallelFor//继承该接口实现并行
	{
		public float deltaTime;
		public float3 playerPos;
		//记得声明读写关系
		public ComponentDataArray<Position> positions;
		[ReadOnly] public ComponentDataArray<VelocityComponent> velocities;

		//会被不同的线程调用，所以方法中不能存在引用类型
		public void Execute(int i)
		{
			//Read
			float3 position = positions[i].Value;
			float speed = velocities[i].Value;
			//算出朝向玩家的向量
			float3 vector = playerPos - position;
			vector = math.normalize(vector);
			float3 newPos = position + vector * speed * deltaTime;
			//Wirte
			positions[i] = new Position { Value = newPos };
		}
	}

	//每帧调用
	protected override JobHandle OnUpdate(JobHandle inputDeps)
	{
		float3 playerPos = playerGroup.GetComponentDataArray<Position>()[0].Value;

		EnemyMoveJob job = new EnemyMoveJob
		{
			deltaTime = Time.deltaTime,
			playerPos = playerPos,
			//声明了组件后，Get时会进行组件的获取
			positions = enemyGroup.GetComponentDataArray<Position>(),
			velocities = enemyGroup.GetComponentDataArray<VelocityComponent>()
		};

		//第一个参数意味着每个job.Execute的执行次数
		return job.Schedule(enemyGroup.CalculateLength(), 64, inputDeps);
	}
}
