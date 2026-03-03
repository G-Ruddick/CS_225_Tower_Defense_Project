using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    [System.Serializable]
    public struct towerStats
    {
        public float range;
        public float damage;
        public float fireRate;
    }

    [SerializeField] private string enemyTag = "Enemy";

    protected Transform target;

    protected float range;
    protected float damage;
    protected float fireRate;

    private float fireCountdown;

    protected virtual void Awake()
    {
        applyStats(getStats());
    }

    protected virtual void Update()
    {
        if (target == null)
        {
            findTarget();
            return;
        }

        if (Vector3.Distance(transform.position, target.position) > range)
        {
            target = null;
            return;
        }

        if (fireCountdown <= 0f)
        {
            shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    protected abstract towerStats getStats();

    protected virtual void applyStats(towerStats stats)
    {
        range = stats.range;
        damage = stats.damage;
        fireRate = Mathf.Max(0.01f, stats.fireRate);
    }

    protected virtual void findTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        Transform nearest = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearest = enemy.transform;
            }
        }

        if (nearest != null && shortestDistance <= range)
            target = nearest;
    }

    protected virtual void shoot()
    {
        if (target == null) return;

        Enemy enemyComponent = target.GetComponent<Enemy>();

        if (enemyComponent != null)
            enemyComponent.takeDamage(damage);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range > 0 ? range : 5f);
    }
}