using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    [SerializeField] private LayerMask towerLayer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) //right click to delete
        {
            trySellTower();
        }
    }

    private void trySellTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 500f, towerLayer))
        {
            //Debug.Log("hit object: " + hit.collider.gameObject.name);

            TowerSeller tower = hit.collider.GetComponentInParent<TowerSeller>();

            if (tower != null)
            {
                //Debug.Log("selling tower: " + tower.gameObject.name);
                tower.sell();
            }
        }
    }
}