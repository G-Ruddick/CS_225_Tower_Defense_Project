using UnityEngine;

public class TowerSeller : MonoBehaviour
{
    private int cost;

    [Header("Sell Settings")]
    [SerializeField] private float refundPercent = 0.75f;

    private void Awake()
    {
        TowerCost costComponent = GetComponent<TowerCost>();

        if (costComponent != null)
        {
            cost = costComponent.getCost();
        }
        else
        {
            //Debug.LogWarning("missing TowerCost on " + gameObject.name);
            cost = 0;
        }
    }

    public void sell()
    {
        int refundAmount = Mathf.RoundToInt(cost * refundPercent);

        if (MoneyManager.instance != null)
        {
            MoneyManager.instance.addMoney(refundAmount);
        }

        Debug.Log(gameObject.name + " sold for $" + refundAmount + " (original cost: " + cost + ")");

        Destroy(gameObject);
    }
}