using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

public class EnemySpawnSystem : ComponentSystem 
{
	private EntityManager entityManager;
	private MeshInstanceRenderer enemyLook;
	private int count;

	public void Init(EntityManager entityManager, MeshInstanceRenderer enemyLook)
	{
		this.entityManager = entityManager;
		this.enemyLook = enemyLook;
		this.count = 5000;
	}

	public struct Group
	{
		public readonly int Length;

		public ComponentDataArray<Position> Positions;
	}

	//Inject标签会使Unity自动注入我们声明的结构中的属性
	[Inject] Group data;

	protected override void OnUpdate()
	{
		if (count > 0) 
		{
			CreatEnemy(count);
			count = 0;
		}
	}

	private void CreatEnemy(int count)
	{
		float3 playerPos = data.Positions[0].Value;

		for (int i = 0; i < count; i++)
		{
			Entity enemyEntity = entityManager.CreateEntity();

			int angle = UnityEngine.Random.Range(1, 360);
			float distance = UnityEngine.Random.Range(15f, 25f);

			float y = Mathf.Sin(angle) * distance;
			float x = y / Mathf.Tan(angle);
			float3 position = new Vector3(playerPos.x + x, playerPos.y + y, 0);
			
			entityManager.AddComponentData(enemyEntity, new EnemyComponent{});
			entityManager.AddComponentData(enemyEntity, new Position{ Value = position });
			entityManager.AddComponentData(enemyEntity, new VelocityComponent{ Value = 1 });
			entityManager.AddSharedComponentData(enemyEntity, enemyLook);
		}
	}
}
