using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.UI;

[AlwaysUpdateSystem]//持续更新系统
public class UISystem : ComponentSystem 
{
	private Text enemyCountText;

	public void Init(Text enemyCountText) => this.enemyCountText = enemyCountText;

	public struct Group
	{
		public readonly int Length;

		public ComponentDataArray<EnemyComponent> Inputs;
	}

	//Inject标签会使Unity自动注入我们声明的结构中的属性
	[Inject] Group data;

	protected override void OnUpdate()
	{
		enemyCountText.text = data.Length.ToString();
	}
}
