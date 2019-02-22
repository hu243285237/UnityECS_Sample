using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using UnityEngine.UI;
using Unity.Mathematics;

public class Bootstrap 
{
	//所有实体的管理器，提供操作Entity的API
	private static EntityManager entityManager;
	//Entity原型，可以看成由组件组成的数组
	//private static EntityArchetype playerArchetype;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	public static void Awake () 
	{
		entityManager = World.Active.GetOrCreateManager<EntityManager>();
	
		//需要命名空间Unity.Transforms
		//playerArchetype = entityManager.CreateArchetype(typeof(Position));
	}
	
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	public static void Start () 
	{
		//把GameObject.Find放在这里因为场景加载完成前无法获取游戏物体
		GameObject playerGo = GameObject.FindWithTag("Player");
		GameObject enemy = GameObject.FindWithTag("Enemy");
		Text enemyCountText = GameObject.Find("Text").GetComponent<Text>();

		//Player MeshInstanceRenderer 需要命名空间Unity.Rendering
		MeshInstanceRenderer playerRenderer = playerGo.GetComponent<MeshInstanceRendererComponent>().Value;
		//获取到渲染数据后可以销毁空物体
		Object.Destroy(playerGo);

		//获取Enemy MeshInstanceRenderer
		MeshInstanceRenderer enemyRenderer = enemy.GetComponent<MeshInstanceRendererComponent>().Value;
		Object.Destroy(enemy);

		//初始化玩家实体
		Entity playerEntity = entityManager.CreateEntity();
		//添加Component组件
		entityManager.AddComponentData(playerEntity, new InputComponent{});
		entityManager.AddComponentData(playerEntity, new Position{ Value = new float3(0, 0, 0)});
		entityManager.AddComponentData(playerEntity, new VelocityComponent{ Value = 7 });
		//向实体添加共享数据组件
		entityManager.AddSharedComponentData(playerEntity, playerRenderer);
	
		//初始化UI系统
		UISystem uISystem = World.Active.GetOrCreateManager<UISystem>();
		uISystem.Init(enemyCountText);
		//初始化敌人生成系统
		EnemySpawnSystem enemySpawnSystem = World.Active.GetOrCreateManager<EnemySpawnSystem>();
		enemySpawnSystem.Init(entityManager, enemyRenderer);
	}
}
