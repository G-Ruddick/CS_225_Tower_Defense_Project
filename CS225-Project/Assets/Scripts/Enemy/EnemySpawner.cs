using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public PathMaking pathMaking;
    public GameObject enemyBase;
    public GameObject enemyFast;
    public GameObject enemySlow;

    Vector3 startingPoint;
    
    void Start() {
        startingPoint = new Vector3(pathMaking.startingTile[1] * 5, 1.05f, pathMaking.startingTile[0] * -5);
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy() {
        // spawning in new enemys
        while (1 == 1) {
            while (PauseButton.gamePaused) {
                yield return null;
            }
            // adding a random spawn point variation
            Vector3 randomSpawnChange = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));
            
            int rand = Random.Range(0,5);
            if (rand == 1) {
                Instantiate(enemyFast, startingPoint + randomSpawnChange, Quaternion.identity);
            }
            else if (rand == 2) {
                Instantiate(enemySlow, startingPoint + randomSpawnChange, Quaternion.identity);
            }
            else {
                Instantiate(enemyBase, startingPoint + randomSpawnChange, Quaternion.identity);
            }

            // wait time between enemies
            yield return new WaitForSeconds(Random.Range(.2f, 1.5f));
        }
    }
}
