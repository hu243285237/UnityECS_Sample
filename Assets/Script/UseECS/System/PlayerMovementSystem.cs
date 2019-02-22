using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class PlayerMovementSystem : ComponentSystem
 {
	 //声明一个结构，其中包含我们定义的过滤条件
	public struct Group
	{
		public readonly int Length;

		public ComponentDataArray<Position> Positions;
		public ComponentDataArray<InputComponent> Inputs;
		public ComponentDataArray<VelocityComponent> Veloctites;
	}

	//声明结构类型的字段，并且加上[Inject]
	[Inject] Group data;

	 protected override void OnUpdate()
	 {
		float deltaTime = Time.deltaTime;

		for (int i = 0; i < data.Length; i++)
		{
			float3 pos = data.Positions[i].Value;
			float3 input = data.Inputs[i].Value;
			float3 velocity = data.Veloctites[i].Value;

			pos += input * velocity * deltaTime;

			data.Positions[i] = new Position { Value = pos };
		}
	 }
}
