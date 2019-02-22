using UnityEngine;

public class EnemySpawn : MonoBehaviour
 {
	[HideInInspector]
	public int EnemyCount;
	[SerializeField]
	private GameObject enemyPrefab;
	private Player player;
	private int count = 5000;

	void Start () => player = GameObject.FindWithTag("Player").GetComponent<Player>();
	
	void Update () 
	{
		if (count > 0)
		{
			Spawn(count);
			count = 0;
		}
	}

	void Spawn(int count)
	{
		Vector3 playerPos = player.transform.position;

		for (int i = 0; i < count; i++)
		{
			GameObject enemy = Instantiate(enemyPrefab);
			EnemyCount++;

			int angle = Random.Range(1, 360);
			float distance = Random.Range(15f, 25f);

			float y = Mathf.Sin(angle) * distance;
			float x = y / Mathf.Tan(angle);

			enemy.transform.position = new Vector3(playerPos.x + x, playerPos.y + y, 0);
			Enemy enemyScript = enemy.AddComponent<Enemy>();
			enemyScript.Init(this, 2.5f, player);
		}
	}
}
