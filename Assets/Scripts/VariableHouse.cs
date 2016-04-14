using UnityEngine;
using System.Collections;

public class VariableHouse{}

public enum units
{
	SIMPLE_GROUND_UNIT,
}

[System.Serializable]
public class BaseUnitVariables
{
	public GameObject prefab;

	public Unit.faction faction;
	public Unit.movementType moveType;

	public int deathValue;

	[Header("Health ")]
	public float maxHealth;
	public float regenRate;

	[Header("Movement")]
	public float moveSpeed;

	[Header("Attack Stats")]
	public float damage;
	public float cooldown;
}

[System.Serializable]
public class EnemyUnitVariables
{
	public Transform waypointParent;
	public BaseUnitVariables baseStats;

}

[System.Serializable]
public class BaseTowerVariables
{
	public float damage;
	public bool areaAttack;
	public float damageReduction;
	public float radius;
	public float reductionDist;
}


[System.Serializable]
public class CameraSettingsVariables
{
	public float moveSpeed;
	public float lowZoomY;
	public float highZoomY;
	public float zoomSpeed;
	[Header("Boundries")]
	public float xBoundry;
	public float yBoundry;
}