using UnityEngine;
using TMPro;

public class Healthandgolddisplay : MonoBehaviour {
    public TMP_Text healthText;
    public TMP_Text moneyText;
    public MoneyManager moneyManager;
    public HealthManager healthManager;

    void Update() {
        moneyText.text = "Money: " + moneyManager.getMoney();
        healthText.text = "Health: " + healthManager.getPlayerHealth();
    }
}
