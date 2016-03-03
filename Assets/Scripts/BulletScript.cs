using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float bulletSpeed = 1;
    public float bulletDamage = 1;
    private TowerScript parentTower;
    private Transform targetTranform;
    private string enemyTag = "Enemy";

    public void Setup(TowerScript parent, Transform target, float speed, float damage) 
    {
        parentTower = parent;
        targetTranform = target;
        bulletSpeed = speed;
        bulletDamage = damage;
    }
	
	void Update () 
    {
	    if (targetTranform != null) 
        {
	        transform.position = Vector3.MoveTowards(transform.position, targetTranform.position, bulletSpeed * Time.deltaTime);
	    }
	}

    void OnTriggerEnter2D(Collider2D otherCollider) 
    {
        if (otherCollider.gameObject.tag == enemyTag) 
        {
            parentTower.RemoveDeadBullet(transform);
            Destroy(gameObject); //TODO implement some sort of method for all moral units to take damage
        }
    }


}
