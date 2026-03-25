using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public PathMaking pathMaking;
    public GameObject enemyBase;
    public GameObject enemyFast;
    public GameObject enemySlow;

    public float minTime;
    public float maxTime;
    public int enemiesSpawned;
    public int interval;

    Vector3 startingPoint;
    
    void Start() {
        minTime = 1.25f;
        maxTime = 2.0f;
        enemiesSpawned = 0;
        interval = 10;
        
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

            enemiesSpawned++;

            // making enemies spawn faster over time
            if (enemiesSpawned % interval == 0) {
                minTime *= .75f;
                maxTime *= .75f;
                interval *= 2;
            }

            // wait time between enemies
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }
}
