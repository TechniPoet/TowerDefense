using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	[Header("Units")]
	public EnemyUnitVariables enemyUnit;
	public static EnemyUnitVariables _EnemyUnit;

	public BaseUnitVariables alliedUnit;
	public static BaseUnitVariables _AlliedUnit;

	[Header("Towers")]
	public BaseTowerVariables baseTower;
	public static BaseTowerVariables _BaseTower;

	[Header("Camera Settings")]
	public CameraSettingsVariables camSettings;
	public static CameraSettingsVariables _CamSettings;
	
	// Use this for initialization
	void Start () {
		_EnemyUnit = enemyUnit;
		_AlliedUnit = alliedUnit;
		_BaseTower = baseTower;
		_CamSettings = camSettings;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
