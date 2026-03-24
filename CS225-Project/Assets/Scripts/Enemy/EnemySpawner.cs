using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public PathMaking pathMaking;
    public GameObject enemy;

    Vector3 startingPoint;
    
    void Start() {
        startingPoint = new Vector3(pathMaking.startingTile[1] * 10, 1.05f, pathMaking.startingTile[0] * -10);
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy() {
        // spawning in new enemys
        while (1 == 1) {
            while (PauseButton.gamePaused) {
                yield return null;
            }
            // adding a random spawn point variation
            Vector3 randomSpawnChange = new Vector3(Random.Range(-3.5f, 3.5f), 0, Random.Range(-3.5f, 3.5f));
            
            Instantiate(enemy, startingPoint + randomSpawnChange, Quaternion.identity);

            // wait time between enemies
            yield return new WaitForSeconds(Random.Range(.2f, 1.5f));
        }
    }
}
