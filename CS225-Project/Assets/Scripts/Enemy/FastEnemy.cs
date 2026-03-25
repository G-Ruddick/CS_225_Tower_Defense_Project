using UnityEngine;

public class FastEnemy : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();

        health = 60f;
        moveSpeed = 3f;
    }

    protected override void die()
    {
        Debug.Log(gameObject.name + " died");
        base.die();
    }
}