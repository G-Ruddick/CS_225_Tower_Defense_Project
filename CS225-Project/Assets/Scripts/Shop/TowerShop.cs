using UnityEngine;

public class TowerShop : MonoBehaviour
{
    [SerializeField] private GameObject cannonTowerPrefab;
    [SerializeField] private GameObject rapidFireTowerPrefab;
    [SerializeField] private GameObject sniperTowerPrefab;

    public void buyCannonTower()
    {
        TowerBuilder.instance.selectTower(cannonTowerPrefab);
    }

    public void buyRapidFireTower()
    {
        TowerBuilder.instance.selectTower(rapidFireTowerPrefab);
    }

    public void buySniperTower()
    {
        TowerBuilder.instance.selectTower(sniperTowerPrefab);
    }
}