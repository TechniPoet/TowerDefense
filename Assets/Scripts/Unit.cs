using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;

public class Unit : Mortal {



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
    private bool isSlowed = false;
    private float slowFactor = 1;


	Quaternion direction;

	void Awake(){

		//mvt = (int)movementType.ground;
		isDead = false;
		target = new List<Vector3> ();
		occupied = false;

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(Input.mousePosition);
		Debug.Log(Input.GetMouseButtonDown(0));
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
		}

		//if there are still target positions in the path and the unit is not occupied then move towards 
		//the first position in a list of targets
		if (target.Count != 0 && !occupied) {

			transform.position = Vector3.MoveTowards(transform.position, target[0], speed * Time.deltaTime * slowFactor);

			//if the unit has reached the first position then remove that position from the list of targets
			if (/*transform.position.Equals(target [0])*/ Vector3.Distance(transform.position, target[0]) < .2f) {
				target.RemoveAt(0);
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

    public void Slow(float slowDuration, float slowFactor) {
        StopCoroutine("SlowLifetime");
        StartCoroutine(SlowLifetime(slowDuration, slowFactor));
    }

    IEnumerator SlowLifetime(float slowDuration, float slowFactor) {
        isSlowed = true;
        this.slowFactor = slowFactor;
        yield return new WaitForSeconds(slowDuration);
        isSlowed = false;
        this.slowFactor = 1;
    }
}
