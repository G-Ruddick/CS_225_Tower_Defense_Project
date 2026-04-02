using UnityEngine;

public class ProjectileBase : MonoBehaviour {
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 2f;

    private Transform target;
    private float damage;

    private Vector3 lastDirection;

    private void Start() {
        Destroy(gameObject, lifeTime);
    }

    public void setTarget(Transform newTarget, float newDamage) {
        target = newTarget;
        damage = newDamage;

        if (target != null)
            lastDirection = (target.position - transform.position).normalized;
    }

    private void Update() {
        if (PauseButton.gamePaused) return;

        if (target != null) {
            lastDirection = (target.position - transform.position).normalized;
        }

        float distanceThisFrame = speed * Time.deltaTime;

        //move forward using last known direction
        transform.Translate(lastDirection * distanceThisFrame, Space.World);
        transform.forward = lastDirection;

        //hit check (distance-based)
        if (target != null) {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget < 0.5f) {
                hitTarget();
            }
        }
    }

    private void hitTarget() {
        if (target != null) {
            EnemyBase enemy = target.GetComponentInParent<EnemyBase>();

            if (enemy != null) {
                enemy.takeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}