using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

    public int radius = 1;

    private CircleCollider2D circleCollider2D;
    private ParticleSystem particleSystem;

	// Use this for initialization
	void Start () {
	    particleSystem = GetComponent<ParticleSystem>();

	}

    void OnTriggerEnter2D(Collider2D coll) {
        
    }
	
}
