using UnityEngine;

public class HealthManager : MonoBehaviour {
    public GameObject gameOverScreen;

    private int playerHealth = 50;

    public void loseHealth(int healthDecrease) {
        playerHealth -= healthDecrease;
    }

    public int getPlayerHealth() {
        return playerHealth;
    }

    void Awake() {
        gameOverScreen.SetActive(false);
    }

    void Update() {
        if (playerHealth <= 0) {
            Debug.Log("Out of HP");
            gameOverScreen.SetActive(true);
        }
    }
}