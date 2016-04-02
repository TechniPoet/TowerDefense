using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Squad : MonoBehaviour {
	
	public Vector3 rallyPoint;
	public AlliedUnit unitPrefab;
	public AlliedUnit alpha;
	public AlliedUnit beta;
	public AlliedUnit gamma;

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

		alphaPos = new Vector3 (rallyPoint.x, rallyPoint.y + 0.5f, 0);
		betaPos = new Vector3 (rallyPoint.x - 0.5f, rallyPoint.y - 0.5f, 0);
		gammaPos = new Vector3 (rallyPoint.x + 0.5f, rallyPoint.y - 0.5f, 0);

		alpha = (AlliedUnit)Instantiate (unitPrefab);
		beta = (AlliedUnit)Instantiate (unitPrefab);
		gamma = (AlliedUnit)Instantiate (unitPrefab);

		alpha.transform.position = alphaPos;
		beta.transform.position = betaPos;
		gamma.transform.position = gammaPos;

		alpha.rallyPos = alphaPos;
		beta.rallyPos = betaPos;
		gamma.rallyPos = gammaPos;
	}
	
	// Update is called once per frame
	void Update () {
		alphaPos = new Vector3 (rallyPoint.x, rallyPoint.y + 0.5f, 0);
		betaPos = new Vector3 (rallyPoint.x - 0.5f, rallyPoint.y - 0.5f, 0);
		gammaPos = new Vector3 (rallyPoint.x + 0.5f, rallyPoint.y - 0.5f, 0);
		
		if (alpha != null)
		{
			alpha.rallyPos = alphaPos;
		}
		else if (!spawningAlpha)
		{
			spawningAlpha = true;
			StartCoroutine(SpawnNewUnit(units.alpha));
		}
		if (beta != null)
		{
			beta.rallyPos = betaPos;
		}
		else if (!spawningBeta)
		{
			spawningBeta = true;
			StartCoroutine(SpawnNewUnit(units.beta));
		}
		if (gamma != null)
		{
			gamma.rallyPos = gammaPos;
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
		AlliedUnit spwned;
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
		// do setup here
	}
}
