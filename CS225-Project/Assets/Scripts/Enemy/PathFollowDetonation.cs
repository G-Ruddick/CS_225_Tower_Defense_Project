using UnityEngine;
using System.Collections;

public class PathFollowDetonation : MonoBehaviour
{
    [Header("Path Settings")]
    public Transform enemyTransform;
    public GameObject Base;
    public PathMaking mapArray;
    public EnemyBase enemyBase;

    public int[] currentTile;

    private bool exploded = false;

    private void Start()
    {
        // Get components and initial tile
        enemyTransform = transform;
        Base = GameObject.Find("Base");
        mapArray = Base.GetComponent<PathMaking>();
        enemyBase = GetComponent<EnemyBase>();

        currentTile = (int[])mapArray.startingTile.Clone();

        StartCoroutine(MoveOnTile());
    }

    private IEnumerator MoveOnTile()
    {
        do
        {
            while (PauseButton.gamePaused)
                yield return null;

            Vector3 currentPosition = enemyTransform.position;
            Vector3 currentDirection;

            // Determine direction based on map
            if (Mathf.Abs(mapArray.map[currentTile[0], currentTile[1]]) == 1)
            {
                currentTile[1]++;
                currentDirection = Vector3.right;
            }
            else if (Mathf.Abs(mapArray.map[currentTile[0], currentTile[1]]) == 2)
            {
                currentTile[1]--;
                currentDirection = Vector3.left;
            }
            else if (Mathf.Abs(mapArray.map[currentTile[0], currentTile[1]]) == 3)
            {
                currentTile[0]--;
                currentDirection = Vector3.forward;
            }
            else
            {
                currentTile[0]++;
                currentDirection = Vector3.back;
            }

            // Move until next tile
            while (Vector3.Distance(enemyTransform.position, currentPosition + currentDirection * 5f) > 0.01f)
            {
                while (PauseButton.gamePaused)
                    yield return null;

                enemyTransform.position = Vector3.MoveTowards(enemyTransform.position,
                    currentPosition + currentDirection * 5f, enemyBase.getMoveSpeed() * 5f * Time.deltaTime);

                yield return null;
            }

        } while (mapArray.map[currentTile[0], currentTile[1]] > 0);

        // Reached end
        if (!exploded)
            Destroy(gameObject);
    }
}