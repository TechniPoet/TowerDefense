using UnityEngine;
using System.Collections;

public class deadZone : MonoBehaviour {
	public delegate void loseHealth(int amt);
	public static event loseHealth LoseHP;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.GetComponent<Unit>() != null)
		{
			LoseHP(1);
			Destroy(col.gameObject);
		}
	}
}
