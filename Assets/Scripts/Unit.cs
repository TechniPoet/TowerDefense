using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Mortal {

	public RectTransform energyBar;
	protected float minX;
	protected float maxX;
	protected float cachedY;

	public Transform sightStart;
	public Transform sightEnd;
	public float vision;

	public float atk = 5;
	public float atkCooldown = 1;
	public bool canAtk = true;

	public float speed;
	public float Speed
	{
		get
		{
			Debug.Log("Slow Factor: "+ slowFactor);
			Debug.Log("Speed: " + (speed * slowFactor));
			return speed * slowFactor; 
		}
	}
	public enum movementType{ ground, fly };
	public movementType movetype;
	//private string direction;

	public enum faction{ player, ai };

	public enum state{ idle, attack, move, enemySighted };
	public state currentState;

	public faction alignment;

	public Vector3 nextTarget;

	//target(s) for unit to move to
	public List<Vector3> target;

	public bool occupied;
	public Unit obstruction;
	protected Unit enemySeen;
    private bool isSlowed = false;
    private float slowFactor = 1;

	string enemyLayer;

	public Transform visionCenter;
	public float visionRadius;
	public bool ignoreVision;

	public Collider2D collision;

	public virtual void Awake(){

		//mvt = (int)movementType.ground;
		
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		energyBar.anchoredPosition = new Vector3(minX + ((currHealth / maxHealth) * energyBar.rect.width), cachedY);
		EngageRaycast ();
		VisionRaycast ();

		ChangeState ();

		switch (currentState) {

		case(state.enemySighted):
			EnemySighted ();
			break;

		case(state.attack):
			if (canAtk) {
				Attack (enemySeen);
			}
			break;

		case(state.move):
			Move ();
			break;

		default:
			Idle ();
			break;

		}

		/*
		//move towards an enemy in view
		if ((enemySeen != null) && !ignoreVision && (obstruction == null) && !occupied) {
			if (!enemySeen.occupied) {
				transform.position = Vector3.MoveTowards (transform.position, enemySeen.sightStart.position, speed * Time.deltaTime);
			} else
				MoveToNext ();
		}else {
			//if there are still target positions in the path and the unit is not occupied then move towards 
			//the first position in a list of targets
			MoveToNext();
		}*/

		if (isDead) {
			
			Destroy (gameObject);
		}
	}
	

	//sets up a unit
	public virtual void Setup (float maxHealth, movementType mvtType, faction faction, float speed, float newAtk, float newCooldown){
		this.maxHealth = maxHealth;
		this.currHealth = maxHealth;
		this.movetype = mvtType;
		this.alignment = faction;
		this.speed = speed;
		this.atk = newAtk;
		this.atkCooldown = newCooldown;

		isDead = false;
		target = new List<Vector3>();
		occupied = false;
		ignoreVision = false;
		currentState = state.idle;

		if (this.gameObject.layer == 8)
		{
			enemyLayer = "Player";
		}
		else
			enemyLayer = "AI";

		visionRadius = Vector3.Distance(sightStart.position, visionCenter.position);

		cachedY = energyBar.anchoredPosition.y;
		maxX = energyBar.anchoredPosition.x;
		minX = energyBar.anchoredPosition.x - energyBar.rect.width;
	}


	/// <summary>
	/// 
	/// </summary>
	public virtual void EngageRaycast(){
		Debug.DrawLine (sightStart.position, sightEnd.position, Color.green);

		collision = Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer(enemyLayer)).collider;

		if (collision != null) {
			Unit hit = collision.gameObject.GetComponent<Unit> ();
			//Debug.Log ("col != null");
			if (hit.alignment != alignment) {
				//Debug.Log ("hit.alignment != alignment");
				if (!hit.occupied && !occupied) {
					hit.occupied = true;
					hit.obstruction = this;
					obstruction = hit;
					occupied = true;
				}
			}
		} else if (obstruction == null) {
			occupied = false;
		} else if (obstruction.collision != null) {
			obstruction.occupied = true;
			obstruction.obstruction = this;
			obstruction = obstruction;
			occupied = true;
		} else if (obstruction.collision == null) {
			obstruction.occupied = false;
			obstruction.obstruction = null;
			obstruction = null;
			occupied = false;
		}

	}

	public virtual void VisionRaycast(){
		DebugExtension.DebugCircle (visionCenter.position, Vector3.back, visionRadius, 0f, false);

		Vector2 origin = new Vector2 (visionCenter.position.x, visionCenter.position.y);

		Collider2D col = Physics2D.CircleCast (origin, visionRadius, origin, 0.0f, 1 << LayerMask.NameToLayer(enemyLayer)).collider;


		if (col != null) {
			enemySeen = col.gameObject.GetComponent<Unit> ();
		} else
			enemySeen = null;
	}

	public virtual void MoveToNext(){
		if (target.Count != 0 && !occupied) {

			transform.position = Vector3.MoveTowards(transform.position, target[0], Speed * Time.deltaTime * slowFactor);

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


	protected void Attack(Mortal target)
	{
		if (canAtk)
		{
			StartCoroutine(StartAtkCoolDown());
			target.takeDamage(atk);
		}
		
	}

	protected IEnumerator StartAtkCoolDown()
	{
		canAtk = false;
		yield return new WaitForSeconds(atkCooldown);
		canAtk = true;
	}

	public virtual void ChangeState (){

		//unit sees an unoccupied enemy
		if ((enemySeen != null) && !ignoreVision && (obstruction == null) && !occupied) {
			currentState = state.enemySighted;
			return;
		}

		//unit is being blocked by an enemy
		if (obstruction != null) {
			currentState = state.attack;
			return;
		}

		//unit is free and has at least 1 target destination
		if (target.Count > 0 && !occupied) {
			currentState = state.move;
			return;
		}

		//unit has nothing to do but comtemplate its meaningless existence
		currentState = state.idle;

	}

	public virtual void EnemySighted(){
		if (!enemySeen.occupied) {
			transform.position = Vector3.MoveTowards (transform.position, enemySeen.sightStart.position,slowFactor* Speed * Time.deltaTime);
		} else
			MoveToNext ();
	}

    public void Slow(float slowDuration, float slowFactor)
	{
        StartCoroutine(SlowLifetime(slowDuration, slowFactor));
    }
	public virtual void Move(){
		MoveToNext ();
	}

    IEnumerator SlowLifetime(float slowDuration, float slowFactor)
	{
		StopCoroutine("SlowLifetime");
		isSlowed = true;
		if (this.slowFactor > slowFactor)
		{
			this.slowFactor = slowFactor;
		}
        yield return new WaitForSeconds(slowDuration);
        isSlowed = false;
        this.slowFactor = 1;
    }
	public virtual void Idle(){
		//do nothing
	}

}
