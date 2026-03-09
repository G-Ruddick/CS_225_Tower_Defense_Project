using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public static TowerBuilder instance;

    [SerializeField] private LayerMask placementMask;
    [SerializeField] private float yOffset = 0f;

    private GameObject selectedTowerPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void selectTower(GameObject towerPrefab)
    {
        selectedTowerPrefab = towerPrefab;
        Debug.Log("selected tower: " + towerPrefab.name);
    }

    private void Update()
    {
        if (selectedTowerPrefab == null) return;

        if (Input.GetMouseButtonDown(0))
            tryPlaceSelectedTower();
    }

    private void tryPlaceSelectedTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 500f, placementMask))
        {
            int cost = getSelectedTowerCost(selectedTowerPrefab);

            if (MoneyManager.instance == null)
            {
                Debug.Log("no moneyManager in scene");
                return;
            }

            if (!MoneyManager.instance.trySpendMoney(cost))
                return;

            Vector3 placePos = hit.point;
            placePos.y += yOffset;

            Instantiate(selectedTowerPrefab, placePos, Quaternion.identity);
            Debug.Log("tower placed: " + selectedTowerPrefab.name + " for $" + cost);
        }
    }

    private int getSelectedTowerCost(GameObject towerPrefab)
    {
        TowerCost costComponent = towerPrefab.GetComponent<TowerCost>();

        if (costComponent == null)
        {
            Debug.Log("tower prefab missing towerCost: " + towerPrefab.name + " (defaulting cost to 0)");
            return 0;
        }

        return costComponent.getCost();
    }
}