using UnityEngine;

public class DetonationEnemy : EnemyBase {
    [Header("Explosion Settings")]
    [SerializeField] private float explodeRadius = 3f;
    [SerializeField] private float explodeDamage = 50f;

    [Header("VFX")]
    [SerializeField] private GameObject explosionPrefab;

    private bool exploded = false;

    private void Update() {
        if (exploded) return;

        //check for towers in radius
        Collider[] hits = Physics.OverlapSphere(transform.position, explodeRadius);
        foreach (Collider hit in hits) {
            TowerBase tower = hit.GetComponent<TowerBase>();
            if (tower != null) {
                Explode();
                break;
            }
        }
    }

    private void Explode() {
        exploded = true;

        //spawn the explosion particle effect
        if (explosionPrefab != null) {
            GameObject effect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            //play the particle system
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();
            if (ps != null)
                ps.Play();

            //destroy the particle after its duration
            float delay = 2f;

            if (ps != null) {
                delay = ps.main.duration + ps.main.startLifetime.constantMax;
            }
            Destroy(effect, delay);
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, explodeRadius);
        foreach (Collider hit in hits) {
            TowerBase tower = hit.GetComponent<TowerBase>();
            if (tower != null) {
                tower.takeDamage(explodeDamage);
            }
        }
        die();
    }

    protected override void die() {
        base.die();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}