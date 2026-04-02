using UnityEngine;

public class TowerCost : MonoBehaviour {
    [SerializeField] private int cost = 50;

    public int getCost() {
        return cost;
    }
}