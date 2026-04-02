using UnityEngine;

public class TowerSelector : MonoBehaviour {
    [SerializeField] private LayerMask towerLayer;

    private void Update() {
        if (Input.GetMouseButtonDown(1)) { //right click to delete
            trySellTower();
        }
    }

    private void trySellTower() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 500f, towerLayer)) {
            TowerSeller tower = hit.collider.GetComponentInParent<TowerSeller>();

            if (tower != null) {
                tower.sell();
            }
        }
    }
}