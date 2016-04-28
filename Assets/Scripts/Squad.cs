using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Squad : MonoBehaviour {
	
	public Vector3 rallyPoint;
	public GameObject unitPrefab;
	public GameObject alpha;
	public GameObject beta;
	public GameObject gamma;

	enum units : int
	{
		alpha = 0,
		beta = 1,
		gamma = 2,
	}
	public float spawnTime = 3;

	bool spawningAlpha, spawningBeta, spawningGamma = false;

	private Vector3 alphaPos;
	private Vector3 betaPos;
	private Vector3 gammaPos;


	// Use this for initialization
	void Awake () {
		rallyPoint = transform.position;
		/*
		alphaPos = new Vector3 (rallyPoint.x, rallyPoint.y + 0.5f, 0);
		betaPos = new Vector3 (rallyPoint.x - 0.5f, rallyPoint.y - 0.5f, 0);
		gammaPos = new Vector3 (rallyPoint.x + 0.5f, rallyPoint.y - 0.5f, 0);

		alpha = Instantiate (unitPrefab);
		beta = Instantiate (unitPrefab);
		gamma = Instantiate (unitPrefab);

		alpha.transform.position = alphaPos;
		beta.transform.position = betaPos;
		gamma.transform.position = gammaPos;

		alpha.GetComponent<AlliedUnit>().rallyPos = alphaPos;
		beta.GetComponent<AlliedUnit>().rallyPos = betaPos;
		gamma.GetComponent<AlliedUnit>().rallyPos = gammaPos;*/
		
	}
	
	// Update is called once per frame
	void Update () {
		alphaPos = new Vector3 (rallyPoint.x, rallyPoint.y + 0.5f, 0);
		betaPos = new Vector3 (rallyPoint.x - 0.5f, rallyPoint.y - 0.5f, 0);
		gammaPos = new Vector3 (rallyPoint.x + 0.5f, rallyPoint.y - 0.5f, 0);
		
		if (alpha != null)
		{
			alpha.GetComponent<AlliedUnit>().rallyPos = alphaPos;
		}
		else if (!spawningAlpha)
		{
			spawningAlpha = true;
			StartCoroutine(SpawnNewUnit(units.alpha));
		}
		if (beta != null)
		{
			beta.GetComponent<AlliedUnit>().rallyPos = betaPos;
		}
		else if (!spawningBeta)
		{
			spawningBeta = true;
			StartCoroutine(SpawnNewUnit(units.beta));
		}
		if (gamma != null)
		{
			gamma.GetComponent<AlliedUnit>().rallyPos = gammaPos;
		}
		else if (!spawningGamma)
		{
			spawningGamma = true;
			StartCoroutine(SpawnNewUnit(units.gamma));
		}
	}

	IEnumerator SpawnNewUnit(units unit)
	{
		yield return new WaitForSeconds(spawnTime);
		GameObject spwned;
		switch(unit)
		{
			case units.alpha:
				spwned = Instantiate(unitPrefab);
				alpha = spwned;
				spawningAlpha = false;
				break;
			case units.beta:
				spwned = Instantiate(unitPrefab);
				beta = spwned;
				spawningBeta = false;
				break;
			case units.gamma:
				spwned = Instantiate(unitPrefab);
				gamma = spwned;
				spawningGamma = false;
				break;
			default:
				throw new System.Exception("unspecified unit attempted to spawn");
		}
		spwned.gameObject.transform.position = rallyPoint;
		BaseUnitVariables u = GameManager._AlliedUnit;
		spwned.GetComponent<AlliedUnit>().Setup(u.maxHealth, u.moveType, u.faction, u.moveSpeed, u.damage, u.cooldown);
	}
}
