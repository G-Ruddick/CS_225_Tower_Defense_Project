using UnityEngine;

public class TowerSeller : MonoBehaviour {
    private int cost;

    [Header("Sell Settings")]
    [SerializeField] private float refundPercent = 0.75f;

    private void Awake() {
        TowerCost costComponent = GetComponent<TowerCost>();

        if (costComponent != null) {
            cost = costComponent.getCost();
        }
        else {
            cost = 0;
        }
    }

    public void sell()
    {
        int refundAmount = Mathf.RoundToInt(cost * refundPercent);

        if (MoneyManager.instance != null) {
            MoneyManager.instance.addMoney(refundAmount);
        }
        Destroy(gameObject);
    }
}