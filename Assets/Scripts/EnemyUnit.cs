﻿using UnityEngine;
using System.Collections;

public class EnemyUnit : Unit {

	public Transform[] wayPoints;
	public Transform wayPointParent;

	// Use this for initialization
	void Start () {
		
		
		
	}

	public void Setup(float maxHealth, movementType mvtType, faction faction, float speed, float newAtk, float newCooldown, Transform pWaypoint)
	{
		base.Awake();
		base.Setup(maxHealth, mvtType, faction, speed, newAtk, newCooldown);
		wayPointParent = pWaypoint;
		for (int i = 0; i < wayPointParent.childCount; i++)
		{
			target.Add(wayPointParent.GetChild(i).position);
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
