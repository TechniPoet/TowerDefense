using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class TowerScript : MonoBehaviour {

    public Transform bulletPrefab;
    public float enemyDetectionRadius = 1f;
    public int maxBulletCount = 5;
	public float bulletCooldown;
    private const string enemyTag = "Enemy";
    private CircleCollider2D circleCollider2D;
    private List<Transform> enemiesInRange;
    private List<Transform> bullets;

	bool canShoot = true;

	void Start () 
    {
        enemiesInRange = new List<Transform>();
        bullets = new List<Transform>();
        circleCollider2D = GetComponent<CircleCollider2D>();
	    circleCollider2D.radius = enemyDetectionRadius;
	}

    void Setup(float detectionRadius, int bulletCount) 
    {
        enemyDetectionRadius = detectionRadius;
        circleCollider2D.radius = enemyDetectionRadius;
        maxBulletCount = bulletCount;
    }

	void Update () 
    {
	    enemiesInRange = enemiesInRange.OrderBy(go => Vector3.Distance(go.transform.position, transform.position)).ToList();
	    var enemyToAtack = enemiesInRange.FirstOrDefault();
	    if (enemyToAtack != null) 
        {
	        AttackEnemy(enemyToAtack);
	    }
	}

    void OnTriggerEnter2D(Collider2D otherCollider) 
    {
        if (otherCollider.gameObject.tag == enemyTag) 
        {
            if (!enemiesInRange.Contains(otherCollider.gameObject.transform)) 
            {
                enemiesInRange.Add(otherCollider.gameObject.transform);
            }
        }
    }

    void OnTriggerExit2D(Collider2D otherCollider) 
    {
        enemiesInRange.Remove(otherCollider.transform);
    }

    void AttackEnemy(Transform enemyTransform) 
    {
        if (bullets.Count < maxBulletCount && canShoot) 
        {
            var bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity) as Transform;
            if (bullet != null) 
            {
                bullets.Add(bullet);
                bullet.GetComponent<BulletScript>().Setup(this, enemyTransform, 5, 1);
				StartCoroutine(CoolDown());
            }
        }
    }

    public void RemoveDeadBullet(Transform bullet) 
    {
        bullets.Remove(bullet);
    }

	IEnumerator CoolDown()
	{
		canShoot = false;
		yield return new WaitForSeconds(bulletCooldown);
		canShoot = true;
	}
}
