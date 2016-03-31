using UnityEngine;
using System.Collections;

public class AreaTowerScript : TowerScript {


    protected override void ProcessAttack() {
        if (enemiesInRange.Count > 0) {
            foreach (var enemy in enemiesInRange) {
                AttackEnemy(enemy);
            }
            StartCoroutine(CoolDown());
        }
    }

}
