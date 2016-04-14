﻿using UnityEngine;
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

	public int deathValue;

	[Header("Health ")]
	public float maxHealth;
	public float startHealth;
	public float regenRate;

	[Header("Movement")]
	public float moveSpeed;
	public Transform wayPoint;

	[Header("Attack Stats")]
	public float damage;
}

[System.Serializable]
public class WeakGroundUnitVariables
{
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
}