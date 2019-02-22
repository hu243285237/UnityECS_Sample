using UnityEngine;
using UnityEngine.UI;

public sealed class UIController : MonoBehaviour 
{
	public Text enemyCountText;
	public EnemySpawn enemySpawn;

	void Update () => enemyCountText.text = enemySpawn.EnemyCount.ToString();
}
