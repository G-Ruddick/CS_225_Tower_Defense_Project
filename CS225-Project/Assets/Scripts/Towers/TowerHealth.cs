using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. Remaining: " + currentHealth);

        if (currentHealth <= 0f)
        {
            die();
        }
    }

    private void die()
    {
        Debug.Log(gameObject.name + " was destroyed by an enemy");
        Destroy(gameObject);
    }
}