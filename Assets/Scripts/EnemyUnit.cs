using UnityEngine;
using System.Collections;

public class EnemyUnit : Unit {

	public Transform[] wayPoints;

	// Use this for initialization
	void Start () {
		base.Awake ();
		for (int i = 0; i < wayPoints.Length; i++)
		{
			target.Add(wayPoints[i].position);
		}
	}
	
	// Update is called once per frame
	public new void Update () {
		base.Update ();

		/*
		if (obstruction != null && enemySeen != null && canAtk)
		{
			Attack(enemySeen);
		}*/
		/*
		//add position to unit's path on mouse click
		if (Input.GetMouseButtonDown(0))
		{
			nextTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			nextTarget.z = 0f;
			target.Add(nextTarget);

			if (target.Count == 1){
				Vector3 dir = transform.position - target[0];
				//Debug.Log(dir.ToString());
				Quaternion rotation = Quaternion.LookRotation(dir);
				rotation.z = 0f;
				rotation.w = 0f;
				//Debug.Log(rotation.ToString());
				transform.rotation = rotation;
				//Debug.Log(transform.rotation.ToString());

				if (transform.position.x > target [0].x) {
					sightEnd.localPosition = new Vector3(vision, 0f, 0f);
					visionCenter.localPosition = new Vector3(visionRadius, 0f, 0f);
				}
				if (transform.position.x < target [0].x) {
					sightEnd.localPosition = new Vector3(vision * -1, 0f, 0f);
					visionCenter.localPosition = new Vector3(visionRadius * -1, 0f, 0f);
				}
			}
		}
		*/
	}
}
