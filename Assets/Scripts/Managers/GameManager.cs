using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	[Header("Units")]
	public WeakGroundUnitVariables weakUnit;
	public static WeakGroundUnitVariables _WeakUnit;

	[Header("Towers")]
	public BaseTowerVariables baseTower;
	public static BaseTowerVariables _BaseTower;

	[Header("Camera Settings")]
	public CameraSettingsVariables camSettings;
	public static CameraSettingsVariables _CamSettings;
	
	// Use this for initialization
	void Start () {
		_WeakUnit = weakUnit;
		_BaseTower = baseTower;
		_CamSettings = camSettings;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
