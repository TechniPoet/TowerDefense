using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float bulletSpeed = 1;
    public float bulletDamage = 1;
    private TowerScript parentTower;
    private Transform targetTranform;
    private string enemyTag = "Enemy";
	private float life = 10;

    public void Setup(TowerScript parent, Transform target, float speed, float damage) 
    {
		Physics2D.IgnoreCollision(parent.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        parentTower = parent;
        targetTranform = target;
		transform.position = parent.transform.position;
        bulletSpeed = speed;
        bulletDamage = damage;
		StartCoroutine(LifeTime());
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
			Die();
        }
    }

	IEnumerator LifeTime()
	{
		yield return new WaitForSeconds(life);
		Die();
	}

	void Die()
	{
		StopAllCoroutines();
		parentTower.RemoveDeadBullet(transform);
		Destroy(gameObject); //TODO implement some sort of method for all moral units to take damage
	}


}
