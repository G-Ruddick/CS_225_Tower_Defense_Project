using UnityEngine;
using System.Collections;

public class TowerHealth : MonoBehaviour {
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] protected float flashDuration = 0.15f;
    private float currentHealth;

    private Renderer towerRenderer;
    private Color originalColor;
    private Coroutine flashCoroutine;

    private void Awake() {
        currentHealth = maxHealth;

        towerRenderer = GetComponent<Renderer>();
        if (towerRenderer != null)
            originalColor = towerRenderer.material.color;
    }

    public void takeDamage(float amount) {
        currentHealth -= amount;

        if (towerRenderer != null) {
            if (flashCoroutine != null)
                StopCoroutine(flashCoroutine);

            flashCoroutine = StartCoroutine(flashRed());
        }
        if (currentHealth <= 0f)
            die();
    }

    protected virtual IEnumerator flashRed() {
        towerRenderer.material.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        towerRenderer.material.color = originalColor;
    }

    private void die() {
        Destroy(gameObject);
    }
}