using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Squad : MonoBehaviour {

	public Vector3 rallyPoint;
	public AlliedUnit unitPrefab;
	public AlliedUnit alpha;
	public AlliedUnit beta;
	public AlliedUnit gamma;

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

		alpha.rallyPos = alphaPos;
		beta.rallyPos = betaPos;
		gamma.rallyPos = gammaPos;
	}
}
