using UnityEngine;

public class SniperTower : TowerBase {
    protected override towerStats getStats() {
        return new towerStats {
            range = 25f,
            damage = 100f,
            fireRate = 0.3f
        };
    }

    protected override void shoot() {
        base.shoot();
    }

    protected override void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range > 0 ? range : 25f);
    }
}