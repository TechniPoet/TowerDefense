using UnityEngine;
using System.Collections;

public class Mortal : MonoBehaviour {

	public int maxHealth;
	protected int currHealth;
	public bool isDead = false;
	
	void update(){

		//kill mortal if health drops to/below zero
		if (currHealth <= 0) {
			isDead = true;
		}

	}

	//deals damage to mortal
	void takeDamage(int dmg){
		currHealth -= dmg;
	}

	//recovers damage for mortal
	void heal(int valueHealed){
		int healed = currHealth + valueHealed;

		if (healed > maxHealth) {
			currHealth = maxHealth;
		} else {currHealth = healed;}
	}
}
