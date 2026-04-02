using UnityEngine;

public class CannonTower : TowerBase {
    protected override towerStats getStats() {
        return new towerStats {
            range = 15f,
            damage = 60f,
            fireRate = 0.5f
        };
    }

    protected override void shoot() {
        base.shoot();
    }

    protected override void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range > 0 ? range : 15f);
    }
}