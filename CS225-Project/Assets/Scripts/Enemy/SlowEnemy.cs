using UnityEngine;

public class SlowEnemy : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();

        health = 200f;
        moveSpeed = 0.75f;
    }

    protected override void die()
    {
        Debug.Log(gameObject.name + " died");
        base.die();
    }
}