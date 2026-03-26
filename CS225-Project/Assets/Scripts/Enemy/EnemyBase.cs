using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float flashDuration = 0.15f;
    [SerializeField] protected int moneyOnDeath = 10;

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

    public virtual void takeDamage(float amount)
    {
        health -= amount;
        //Debug.Log(gameObject.name + " was hit for " + amount + " damage. Remaining health: " + health);

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
        //Debug.Log(gameObject.name + " died");

        //give player coins on enemy death
        if (MoneyManager.instance != null)
        {
            MoneyManager.instance.addMoney(moneyOnDeath);
        }

        Destroy(gameObject);
    }
}