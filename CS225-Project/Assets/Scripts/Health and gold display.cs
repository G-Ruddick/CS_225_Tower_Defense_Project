using UnityEngine;
using TMPro;

public class Healthandgolddisplay : MonoBehaviour {
    public TMP_Text health_Text;
    public TMP_Text money_Text;
    public MoneyManager moneyManager;

    void Update() {
        money_Text.text = "Money: " + moneyManager.getMoney();
    }
}
