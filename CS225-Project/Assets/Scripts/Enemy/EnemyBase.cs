using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float flashDuration = 0.15f;

    private Renderer enemyRenderer;
    private Color originalColor;
    private Coroutine flashCoroutine;

    protected virtual void Awake()
    {
        enemyRenderer = GetComponent<Renderer>();

        if (enemyRenderer != null)
            originalColor = enemyRenderer.material.color;
    }

    public float getMoveSpeed() {
        return moveSpeed;
    }

    // protected virtual void Update()
    // {
    //     moveForward();
    // }

    // // basic movement (replace later)
    // protected virtual void moveForward()
    // {
    //     transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    // }

    public virtual void takeDamage(float amount)
    {
        health -= amount;

        Debug.Log(gameObject.name + " was hit for " + amount + " damage. Remaining health: " + health);

        if (enemyRenderer != null)
        {
            if (flashCoroutine != null)
                StopCoroutine(flashCoroutine);

            flashCoroutine = StartCoroutine(flashRed());
        }

        if (health <= 0f)
            die();
    }

    protected virtual IEnumerator flashRed()
    {
        enemyRenderer.material.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        enemyRenderer.material.color = originalColor;
    }

    protected virtual void die()
    {
        Debug.Log(gameObject.name + " died");
        Destroy(gameObject);
    }
}