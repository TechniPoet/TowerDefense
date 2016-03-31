using UnityEngine;
using System.Collections;

public class NewBehaviourScript : BulletScript {
    public float slowFactor = .5f;
    public float slowDuration = 5f;

    protected override void ProcessHit(GameObject gameObject) {
        gameObject.GetComponent<Unit>().Slow(slowDuration, slowFactor);
    }

}
