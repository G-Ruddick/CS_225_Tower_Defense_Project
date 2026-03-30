using UnityEngine;
using System.Collections;

public class PathFollowBase : MonoBehaviour {
    public Transform enemyTransform;
    public GameObject Base;
    public PathMaking mapArray;
    public EnemyBase enemy;
    public HealthManager healthManager;

    public int [] currentTile;


    void Start() {
        // Getting the game objects and scripts
        enemyTransform = GetComponent<Transform>();
        Base = GameObject.Find("Base");
        mapArray = Base.GetComponent<PathMaking>();
        enemy = GetComponent<EnemyBase>();

        healthManager = GameObject.Find("Health Manager").GetComponent<HealthManager>();

        currentTile = (int[]) mapArray.startingTile.Clone();

        StartCoroutine(moveOnTile());
    }

    IEnumerator moveOnTile() {
        do {
            // checking if the game is paused
            while (PauseButton.gamePaused) {
                yield return null;
            }

            Vector3 currentPosition = enemyTransform.position;
            Vector3 currentDirection;

            if (Mathf.Abs(mapArray.map[currentTile[0], currentTile[1]]) == 1) {
                currentTile[1]++;
                currentDirection = Vector3.right;
            }
            else if (Mathf.Abs(mapArray.map[currentTile[0], currentTile[1]]) == 2) {
                currentTile[1]--;
                currentDirection = Vector3.left;
            }
            else if (Mathf.Abs(mapArray.map[currentTile[0], currentTile[1]]) == 3) {
                currentTile[0]--;
                currentDirection = Vector3.forward;
            }
            else {
                currentTile[0]++;
                currentDirection = Vector3.back;
            }

            // moving the enemy at a fixed speed and direction until the reach the next tile
            while (Vector3.Distance(enemyTransform.position, currentPosition + currentDirection * 5f) > 0.01f) {
                while (PauseButton.gamePaused) {
                    yield return null;
                }
                
                enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, 
                    currentPosition + currentDirection * 5f, enemy.getMoveSpeed() * 5f * Time.deltaTime);

                yield return null;
            }
        }
        while (mapArray.map[currentTile[0], currentTile[1]] > 0);

        // killing the enemy if they made it to the end
        Destroy(this.gameObject);
        healthManager.loseHealth(enemy.getPlayerHealthRemoval());
    }
}
