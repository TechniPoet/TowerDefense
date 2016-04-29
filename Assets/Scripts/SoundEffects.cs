using UnityEngine;
using System.Collections;

public class SoundEffects : MonoBehaviour {

	public AudioClip enemyDie;
	public AudioClip allyDie;
	public AudioClip enemyAttack;
	public AudioClip allyAttack;
	public AudioClip towerAttack;
	public AudioClip slowTowerAttack;
	public AudioClip towerBuild;

	AudioSource audio;

	// Use this for initialization
	void Awake () {
		audio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {

	}

	public void enemyDieSound(){
		audio.PlayOneShot (enemyDie);
	}

	public void allyDieSound(){
		audio.PlayOneShot (allyDie);
	}

	public void enemyAttackSound(){
		audio.PlayOneShot (enemyAttack);
	}

	public void allyAttackSound(){
		audio.PlayOneShot (allyAttack);
	}

	public void towerAttackSound(){
		audio.PlayOneShot (towerAttack);
	}

	public void slowTowerAttackSound(){
		audio.PlayOneShot (slowTowerAttack);
	}

	public void towerBuildSound(){
		audio.PlayOneShot (towerBuild);
	}
}
