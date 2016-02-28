using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Mortal {



	public float speed;
	public enum movementType{ ground, fly };
	//private string direction;

	private Vector3 nextTarget;

	//target(s) for unit to move to
	private List<Vector3> target;

	public bool occupied;
	private Unit obstruction;

	void Awake(){

		isDead = false;
		target = new List<Vector3> ();
		occupied = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (obstruction != null) {
			if (obstruction.isDead) {
				occupied = false;
			}
		}

		//add position to unit's path on mouse click
		if (Input.GetMouseButtonDown(0))
		{
			nextTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			nextTarget.z = 0f;
			target.Add(nextTarget);
		}

		//if there are still target positions in the path and the unit is not occupied then move towards 
		//the first position in a list of targets
		if (target.Count != 0 && !occupied) {

			transform.position = Vector3.MoveTowards(transform.position, target[0], speed * Time.deltaTime);

			//if the unit has reached the first position then remove that position from the list of targets
			if (transform.position.Equals(target [0])) {
				target.RemoveAt(0);
			}
		}

		if (isDead == true) {
			
			Destroy (gameObject);
		}


	}

	//sets up a unit
	void Setup (int maxHealth, int mvtType, float speed){
		this.maxHealth = maxHealth;
		this.currHealth = maxHealth;
		//movementType + mvtType;
		this.speed = speed;
	}

	void OnTriggerEnter2D(Collider2D col){
		Unit hit = col.gameObject.GetComponent<Unit>();

		Debug.Log (hit.occupied);

		if (!hit.occupied && !occupied) {
			hit.occupied = true;
			hit.obstruction = this;
			obstruction = hit;
			occupied = true;
		}
	}


}
