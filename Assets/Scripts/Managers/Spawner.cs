using UnityEngine;
using System.Collections;
using m = GameManager;

public class Spawner : MonoBehaviour {

	public void SpawnSimpleGroundUnit(Vector2 pos)
	{
		EnemyUnitVariables e = m._EnemyUnit;
		GameObject temp = Instantiate(e.baseStats.prefab, pos, Quaternion.identity) as GameObject;
		temp.GetComponent<EnemyUnit>().Setup(e.baseStats.maxHealth,
			e.baseStats.moveType, e.baseStats.faction, e.baseStats.moveSpeed, e.baseStats.damage,
			e.baseStats.cooldown, e.waypointParent);
	}
}
