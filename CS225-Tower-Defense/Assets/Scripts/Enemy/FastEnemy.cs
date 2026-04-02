using UnityEngine;

public class FastEnemy : EnemyBase {
    protected override void Awake() {
        base.Awake();

        health = 60f;
        moveSpeed = 3f;
        playerHealthRemoval = 1;
    }

    protected override void die(){
        base.die();
    }
}