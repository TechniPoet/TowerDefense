using UnityEngine;
using System.Collections;

public class AlliedUnit : Unit {

	public Vector3 rallyPos;

	// Use this for initialization
	void Start () {
		base.Awake ();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();

		/*
		if (obstruction == null) {
			if (enemySeen == null && !occupied) {
				transform.position = Vector3.MoveTowards (transform.position, rallyPos, speed * Time.deltaTime);
			}
			if (enemySeen != null) {
				if (!occupied && enemySeen.occupied) {
					transform.position = Vector3.MoveTowards (transform.position, rallyPos, speed * Time.deltaTime);
				}
			}
		}
		else
		{
			if (canAtk)
			{
				Attack(enemySeen);
			}
		}*/
	}

	public override void EngageRaycast ()
	{
		base.EngageRaycast ();
	}

	public override void VisionRaycast ()
	{
		base.VisionRaycast ();
	}

	public override void Idle ()
	{
		//if not in formation then move into formation
		if (!transform.position.Equals(rallyPos))
			transform.position = Vector3.MoveTowards (transform.position, rallyPos, speed * Time.deltaTime);
	}

	public override void EnemySighted ()
	{
		base.EnemySighted ();

		//if sighted enemy is already occupied, then move back into formation
		if (enemySeen.occupied) {
			transform.position = Vector3.MoveTowards (transform.position, rallyPos, speed * Time.deltaTime);
		}
	}
}
