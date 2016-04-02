using UnityEngine;
using System.Collections;

public class Mortal : MonoBehaviour {

	public float maxHealth = 100;
	public float currHealth = 100;
	public bool isDead = false;
	
	public virtual void Update(){

		//kill mortal if health drops to/below zero
		if (currHealth <= 0) {
			isDead = true;
		}

	}

	//deals damage to mortal
	public void takeDamage(float dmg){
		currHealth -= dmg;
	}

	//recovers damage for mortal
	void heal(int valueHealed){
		currHealth += valueHealed;
		currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
	}
}
