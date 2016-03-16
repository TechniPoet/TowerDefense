using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Mortal {


	public Transform sightStart;
	public Transform sightEnd;
	public float vision;

	public float speed;
	public enum movementType{ ground, fly };
	public movementType movetype;
	//private string direction;

	public enum faction{ player, ai };
	public faction alignment;

	public Vector3 nextTarget;

	//target(s) for unit to move to
	public List<Vector3> target;

	public bool occupied;
	private Unit obstruction;
	private Unit enemySeen;

	private string enemyLayer;

	public Transform visionCenter;
	public float visionRadius;

	public virtual void Awake(){

		//mvt = (int)movementType.ground;
		isDead = false;
		target = new List<Vector3> ();
		occupied = false;

		if (this.gameObject.layer == 8) {
			enemyLayer = "Player";
		} else
			enemyLayer = "AI";

		visionRadius = Vector3.Distance(sightStart.position, visionCenter.position);
	}
	
	// Update is called once per frame
	public virtual void Update () {

		EngageRaycast ();
		VisionRaycast ();

		//Debug.Log(Input.mousePosition);
		//Debug.Log(Input.GetMouseButtonDown(0));
		if (obstruction != null) {
			if (obstruction.isDead) {
				//occupied = false;
			}
		}

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

		//if there are still target positions in the path and the unit is not occupied then move towards 
		//the first position in a list of targets
		if (target.Count != 0 && !occupied) {

			//unit first moves towards an enemy that it sees
			if (enemySeen != null) {
				transform.position = Vector3.MoveTowards(transform.position, enemySeen.sightStart.position, speed * Time.deltaTime);
			}else{

				transform.position = Vector3.MoveTowards(transform.position, target[0], speed * Time.deltaTime);

				//if the unit has reached the first position then remove that position from the list of targets
				if (/*transform.position.Equals(target [0])*/ Vector3.Distance (transform.position, target [0]) < .2f) {
					target.RemoveAt (0);

					if (target.Count != 0) {
						Vector3 dir = transform.position - target [0];
						//Debug.Log(dir.ToString());
						Quaternion rotation = Quaternion.LookRotation (dir);
						rotation.z = 0f;
						rotation.w = 0f;
						//Debug.Log(rotation.ToString());
						transform.rotation = rotation;
						//Debug.Log(transform.rotation.ToString());

						if (transform.position.x > target [0].x) {
							sightEnd.localPosition = new Vector3 (vision, 0f, 0f);
							visionCenter.localPosition = new Vector3 (visionRadius, 0f, 0f);
						}
						if (transform.position.x < target [0].x) {
							sightEnd.localPosition = new Vector3 (vision - 1, 0f, 0f);
							visionCenter.localPosition = new Vector3 (visionRadius * -1, 0f, 0f);
						}
					}
				}
			}
		}

		if (isDead == true) {
			
			Destroy (gameObject);
		}


	}

	//sets up a unit
	void Setup (int maxHealth, movementType mvtType, faction faction, float speed){
		this.maxHealth = maxHealth;
		this.currHealth = maxHealth;
		this.movetype = mvtType;
		this.alignment = faction;
		this.speed = speed;
	}

	void EngageRaycast(){
		Debug.DrawLine (sightStart.position, sightEnd.position, Color.green);

		Collider2D col = Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer(enemyLayer)).collider;

		if (col != null) {
			Unit hit = col.gameObject.GetComponent<Unit> ();
			if (hit.alignment != alignment) {
				if (!hit.occupied && !occupied) {
					hit.occupied = true;
					hit.obstruction = this;
					obstruction = hit;
					occupied = true;
				}
			}
		} else {
			if (obstruction != null) {
				obstruction.occupied = false;
				obstruction.obstruction = null;
			}
			obstruction = null;
			occupied = false;
		}
	}

	void VisionRaycast(){
		DebugExtension.DebugCircle (visionCenter.position, Vector3.back, visionRadius, 0f, false);

		Vector2 origin = new Vector2 (visionCenter.position.x, visionCenter.position.y);

		Collider2D col = Physics2D.CircleCast (origin, visionRadius, origin, Mathf.Infinity, 1 << LayerMask.NameToLayer(enemyLayer)).collider;


		if (col != null) {
			enemySeen = col.gameObject.GetComponent<Unit> ();
		} else
			enemySeen = null;
	}

	/*
	void OnCollisionEnter2D(Collision2D col){
		Unit hit = col.gameObject.GetComponent<Unit>();

		Debug.Log (hit.occupied);

		if (hit.alignment != alignment) {
			if (!hit.occupied && !occupied) {
				hit.occupied = true;
				hit.obstruction = this;
				obstruction = hit;
				occupied = true;
			}
		}
	}

	void OnCollisionExit2D(Collision2D col){

		Unit hit = col.gameObject.GetComponent<Unit> ();

		Debug.Log ("exit collision: " + hit.alignment.ToString());

		if (hit.alignment != alignment) {
			occupied = false;
		}

	}*/


}
