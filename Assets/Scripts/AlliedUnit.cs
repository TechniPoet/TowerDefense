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
	}

	public override void EngageRaycast ()
	{
		base.EngageRaycast ();
	}

	public override void VisionRaycast ()
	{
		base.VisionRaycast ();
	}
}
