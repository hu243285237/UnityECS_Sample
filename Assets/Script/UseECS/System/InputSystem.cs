using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class InputSystem : ComponentSystem 
{
	public struct Group
	{
		public readonly int Length;

		public ComponentDataArray<InputComponent> Inputs;
	}

	//Inject标签会使Unity自动注入我们声明的结构中的属性
	[Inject] Group data;

	protected override void OnUpdate()
	{
		for (int i = 0; i < data.Length; i++)
		{
			float x = Input.GetAxisRaw("Horizontal");
			float y = Input.GetAxisRaw("Vertical");

			float3 normalized = new float3();

			if (x != 0 || y != 0)
			{
				normalized = math.normalize(new float3(x, y, 0));
			}

			data.Inputs[i] = new InputComponent { Value = normalized };
		}
	}
}
