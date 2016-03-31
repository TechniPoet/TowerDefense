using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float bulletSpeed = 1;
    public int bulletDamage = 1;
    private TowerScript parentTower;
    private Transform targetTranform;
    protected string enemyTag = "Enemy";
    private float life = 10;

    public void Setup(TowerScript parent, Transform target, float speed, int damage) {
        Physics2D.IgnoreCollision(parent.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        parentTower = parent;
        targetTranform = target;
        transform.position = parent.transform.position;
        bulletSpeed = speed;
        bulletDamage = damage;
        StartCoroutine(LifeTime());
    }

    void Update() {
        Move();
    }

    protected virtual void Move() {
        if (targetTranform != null) {
            transform.position = Vector3.MoveTowards(transform.position, targetTranform.position, bulletSpeed * Time.deltaTime);
        }

    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == enemyTag) {
            ProcessHit(coll.gameObject);
            Die();
        }
    }

    protected virtual void ProcessHit(GameObject gameObject) {
        gameObject.GetComponent<Mortal>().takeDamage(bulletDamage);       
    }

    IEnumerator LifeTime() {
        yield return new WaitForSeconds(life);
        Die();
    }

    void Die() {
        StopAllCoroutines();
        parentTower.RemoveDeadBullet(transform);
        Destroy(gameObject);
    }


}
