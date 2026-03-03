using UnityEngine;

public class RapidFireTower : TowerBase
{
    protected override towerStats getStats()
    {
        return new towerStats
        {
            range = 10f,
            damage = 8f,
            fireRate = 4f
        };
    }

    protected override void shoot()
    {
        Debug.Log("rapid fire tower");
        base.shoot();
    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range > 0 ? range : 10f);
    }
}