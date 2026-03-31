using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    [SerializeField] private int startingMoney = 225;
    [SerializeField] private int currentMoney;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        currentMoney = startingMoney;
        Debug.Log("starting money: " + currentMoney);
    }

    public int getMoney()
    {
        return currentMoney;
    }

    public void addMoney(int amount)
    {
        currentMoney += amount;
        Debug.Log("money added: +" + amount + " | total: " + currentMoney);
    }

    public bool trySpendMoney(int amount)
    {
        if (currentMoney < amount)
        {
            Debug.Log("not enough money. need: " + amount + " | have: " + currentMoney);
            return false;
        }

        currentMoney -= amount;
        Debug.Log("money spent: -" + amount + " | total: " + currentMoney);
        return true;
    }
}