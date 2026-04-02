using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuilder : MonoBehaviour {
    public static TowerBuilder instance;

    [SerializeField] private LayerMask placementMask;
    [SerializeField] private LayerMask towerMask;
    [SerializeField] private float yOffset = 0f;

    private GameObject selectedTowerPrefab;

    private void Awake() {
        instance = this;
    }

    public void selectTower(GameObject towerPrefab) {
        selectedTowerPrefab = towerPrefab;
    }

    private void Update() {
        if (selectedTowerPrefab == null) return;

        //ignore clicks on UI to prevent placing tower when trying to swap tower placements
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
            tryPlaceSelectedTower();
    }

    private void tryPlaceSelectedTower() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //checking for tower already existsing in spot
        if (Physics.Raycast(ray, out hit, 500f, towerMask)) {}
        
        else if (Physics.Raycast(ray, out hit, 500f, placementMask)) {
            int cost = getSelectedTowerCost(selectedTowerPrefab);

            if (!MoneyManager.instance.trySpendMoney(cost))
                return;

            Vector3 placePos = hit.point;
            placePos.y += yOffset;

            //rounding the x and z to snap to the grid
            placePos.x = Mathf.Round((placePos.x / 5)) * 5;
            placePos.z = Mathf.Round((placePos.z / 5)) * 5;

            Instantiate(selectedTowerPrefab, placePos, Quaternion.identity);
        }
    }

    private int getSelectedTowerCost(GameObject towerPrefab) {
        TowerCost costComponent = towerPrefab.GetComponent<TowerCost>();

        if (costComponent == null) {
            return 0;
        }

        return costComponent.getCost();
    }
}