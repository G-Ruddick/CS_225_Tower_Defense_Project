using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float flashDuration = 0.15f;

    private Renderer enemyRenderer;
    private Color originalColor;
    private Coroutine flashCoroutine;

    private void Awake()
    {
        enemyRenderer = GetComponent<Renderer>();

        if (enemyRenderer != null)
            originalColor = enemyRenderer.material.color;
    }

    public void takeDamage(float amount)
    {
        health -= amount;

        Debug.Log(gameObject.name + " was hit for " + amount + " damage. Remaining health: " + health);

        if (enemyRenderer != null)
        {
            //stop previous flash if already running
            if (flashCoroutine != null)
                StopCoroutine(flashCoroutine);

            //start flash coroutine
            flashCoroutine = StartCoroutine(flashRed());
        }

        if (health <= 0f)
            die();
    }

    //coroutine that changes the enemy color to red
    private IEnumerator flashRed()
    {
        enemyRenderer.material.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        enemyRenderer.material.color = originalColor;
    }

    public void die()
    {
        Debug.Log(gameObject.name + " died");

        Destroy(gameObject);
    }
}