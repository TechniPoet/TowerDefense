using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class TowerScript : MonoBehaviour {

    public Transform bulletPrefab;
    public int bulletDamage = 1;
    public float bulletSpeed = 5;
    public float enemyDetectionRadius = 1f; // Used for both detection and area damage - bad idea?
    public int maxBulletCount = 5;
    public float bulletCooldown;
    private const string enemyTag = "Enemy";
    private CircleCollider2D circleCollider2D;
    protected List<Transform> enemiesInRange;
    private List<Transform> bullets;

    bool canShoot = true;

    void Start() {
        enemiesInRange = new List<Transform>();
        bullets = new List<Transform>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.radius = enemyDetectionRadius;
    }

    void Setup(float detectionRadius, int bulletCount) {
        enemyDetectionRadius = detectionRadius;
        circleCollider2D.radius = enemyDetectionRadius;
        maxBulletCount = bulletCount;
    }

    void Update() {

		enemiesInRange = enemiesInRange.Where(i => i != null).ToList();
        enemiesInRange = enemiesInRange.OrderBy(go => Vector3.Distance(go.transform.position, transform.position)).ToList();
        ProcessAttack();
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.gameObject.tag == enemyTag) {
            if (!enemiesInRange.Contains(otherCollider.gameObject.transform)) {
                enemiesInRange.Add(otherCollider.gameObject.transform);
            }
        }
    }

    void OnTriggerExit2D(Collider2D otherCollider) {
        enemiesInRange.Remove(otherCollider.transform);
    }

    protected virtual void ProcessAttack() {
        var enemyToAtack = enemiesInRange.FirstOrDefault();
        if (enemyToAtack != null) {
			if (canShoot)
			{
				AttackEnemy(enemyToAtack);
				StartCoroutine(CoolDown());
			}
        }
    }

    protected virtual void AttackEnemy(Transform enemyTransform) {
        if (bullets.Count < maxBulletCount && canShoot) {
            CreateBullet(enemyTransform);
        }
    }

    protected Transform CreateBullet(Transform enemyTransform) {
        var bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity) as Transform;
        if (bullet != null) {
            bullets.Add(bullet);
            bullet.GetComponent<BulletScript>().Setup(this, enemyTransform, bulletSpeed, bulletDamage);
        } else {
            Debug.Log("Bullet created as null"); //TODO throw exception maybe?
        }
        return bullet;
    }

    public void RemoveDeadBullet(Transform bullet) {
        bullets.Remove(bullet);
    }

    protected IEnumerator CoolDown() {
        canShoot = false;
        yield return new WaitForSeconds(bulletCooldown);
        canShoot = true;
    }
}
