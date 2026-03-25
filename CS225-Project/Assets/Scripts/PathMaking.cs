using UnityEngine;

/*
directions:
    1. Right
    2. Left
    3. Up
    4. Down
    -1. starting/ending Right
    -2. starting/ending Left
    -3. starting/ending Up
    -4. starting/ending Down
*/

public class PathMaking : MonoBehaviour {
    public GameObject Grass_Tile;
    public GameObject Path_Tile_UD;
    public GameObject Path_Tile_LR;
    public GameObject Path_Tile_UR;
    public GameObject Path_Tile_UL;
    public GameObject Path_Tile_DR;
    public GameObject Path_Tile_DL;
    public GameObject Start_Tile;
    public GameObject End_TIle;

    public int mapHeight;
    public int mapWidth;
    public int[,] map;
    public int[] startingTile = new int[2];

    void Awake() {
        // getting the map height and width from the starting scene
        mapHeight = Heightandwidth.Height.getSize();
        mapWidth = Heightandwidth.Width.getSize();
        // mapHeight = 7;
        // mapWidth = 7;

        map = pathGeneration(mapHeight,mapWidth);
        displayMap(map);
        mapTileCreation(map);
    }

    int[,] pathGeneration(int height, int width) {
        bool validPath = false;
        int[,] tileMap = new int[height, width];
        int pathLength = 0;
        
        // recreates pathing until long enough
        while (pathLength < height * width / 6 || pathLength > height * width / 3) {
            validPath = false;
            pathLength = 0;

            tileMap = new int[height, width];
            int[] currentTile = new int[2];

            // starting tile
            int direction = -Random.Range(1 , 5);
            switch (direction) {
                case -1:
                    currentTile[0] = Random.Range(0 , height);
                    currentTile[1] = 0;
                    break;
                case -2:
                    currentTile[0] = Random.Range(0 , height);
                    currentTile[1] = width - 1;
                    break;
                case -3:
                    currentTile[0] = height - 1;
                    currentTile[1] = Random.Range(0 , width);
                    break;
                case -4:
                    currentTile[0] = 0;
                    currentTile[1] = Random.Range(0 , width);
                    break;
            }

            tileMap[currentTile[0] , currentTile[1]] = direction;
            startingTile[0] = currentTile[0];
            startingTile[1] = currentTile[1];
            
            // moving the current position
            if (direction == -1) {
                currentTile[1]++;
            }
            else if (direction == -2) {
                currentTile[1]--;
            }
            else if (direction == -3) {
                currentTile[0]--;
            }
            else if (direction == -4) {
                currentTile[0]++;
            }

            pathLength++;
            
            // creating the path until its the correct length
            while (!validPath) {
                // picking a direction but is inherently more likely to 
                // pick the same direction
                if (Random.Range(1,4) == 2) {}
                else {
                    direction = Random.Range(1 , 5);
                }

                // checking for the edge and stopping current loop
                if ((currentTile[0] == 0 && direction == 3) ||
                (currentTile[0] == height - 1 && direction == 4) ||
                (currentTile[1] == 0 && direction == 2) ||
                (currentTile[1] == width - 1 && direction == 1)) {
                    tileMap[currentTile[0], currentTile[1]] = -direction;
                    validPath = true;
                    continue;
                }

                // checking if not on the edge so it doesnt look outside the array
                if (currentTile[0] != 0 && currentTile[0] != height - 1 &&
                currentTile[1] != 0 && currentTile[1] != width - 1) {
                    // checking to see if the path is trapped and restarting if so
                    if (tileMap[currentTile[0] , currentTile[1] + 1] != 0 &&
                    tileMap[currentTile[0] , currentTile[1] - 1] != 0 &&
                    tileMap[currentTile[0] + 1 , currentTile[1]] != 0 &&
                    tileMap[currentTile[0] - 1 , currentTile[1]] != 0) {
                        pathLength = 0;
                        break;
                    }
                }
                
                // if not the end of the map check the tile in front if occupied
                // and update the current tile if so
                if (direction == 1) {
                    if (tileMap[currentTile[0] , currentTile[1] + 1] == 0) {
                        tileMap[currentTile[0], currentTile[1]] = direction;
                        currentTile[1]++;
                    }
                    else { continue; }
                }
                else if (direction == 2) {
                    if (tileMap[currentTile[0] , currentTile[1] - 1] == 0) {
                        tileMap[currentTile[0], currentTile[1]] = direction;
                        currentTile[1]--;
                    }
                    else { continue; }
                }
                else if (direction == 3) {
                    if (tileMap[currentTile[0] - 1, currentTile[1]] == 0) {
                        tileMap[currentTile[0], currentTile[1]] = direction;
                        currentTile[0]--;
                    }
                    else { continue; }
                }
                else if (direction == 4) {
                    if (tileMap[currentTile[0] + 1, currentTile[1]] == 0) {
                        tileMap[currentTile[0], currentTile[1]] = direction;
                        currentTile[0]++;
                    }
                    else { continue; }
                }

                pathLength++;
            }
        }
        return tileMap;
    }

    void displayMap(int[,] inputMap) {
        string output = "\n";

        // creating an output display of the map array
        for (int i = 0; i < inputMap.GetLength(0); i++) {
            for (int j = 0; j < inputMap.GetLength(1); j++) {
                output += inputMap[i , j] + " ";
            }
            output += "\n";
        }

        Debug.Log(output);
    }

    void mapTileCreation(int[,] inputMap) {
        // Creating grass tile objects
        for (int r = 0; r < mapHeight; r++) {
            for (int c = 0; c < mapWidth; c++) {
                if (inputMap[r,c] == 0) {
                    Instantiate(Grass_Tile, new Vector3(5f * c, 0, -5f * r), Quaternion.identity);
                }
            }
        }

        int[] nextTile = new int[2];

        int[] currentTile = new int[2];
        currentTile[0] = startingTile[0];
        currentTile[1] = startingTile[1];

        // Starting tile
        Vector3 tilePosition = new Vector3(5f * currentTile[1], 0, -5f * currentTile[0]);
        if (Mathf.Abs(inputMap[currentTile[0], currentTile[1]]) == 1 || 
        Mathf.Abs(inputMap[currentTile[0], currentTile[1]]) == 2 ) {
            Instantiate(Path_Tile_LR, tilePosition, Quaternion.identity);
        }
        else {
            Instantiate(Path_Tile_UD, tilePosition, Quaternion.identity);
        }
        Instantiate(Start_Tile, tilePosition + new Vector3(0, 0.01f, 0), Quaternion.identity);

        // creating tile path objects
        do {
            nextTile[0] = currentTile[0];
            nextTile[1] = currentTile[1];

            if (Mathf.Abs(inputMap[currentTile[0], currentTile[1]]) == 1) {
                nextTile[1]++;
                tilePosition = new Vector3(5f * nextTile[1], 0, -5f * nextTile[0]);

                if (Mathf.Abs(inputMap[nextTile[0], nextTile[1]]) == 1) {
                    Instantiate(Path_Tile_LR, tilePosition, Quaternion.identity);
                }
                else if (Mathf.Abs(inputMap[nextTile[0], nextTile[1]]) == 3) {
                    Instantiate(Path_Tile_DR, tilePosition, Quaternion.identity);
                }
                else if (Mathf.Abs(inputMap[nextTile[0], nextTile[1]]) == 4) {
                    Instantiate(Path_Tile_UR, tilePosition, Quaternion.identity);
                }
            }
            else if (Mathf.Abs(inputMap[currentTile[0], currentTile[1]]) == 2) {
                nextTile[1]--;
                tilePosition = new Vector3(5f * nextTile[1], 0, -5f * nextTile[0]);

                if (Mathf.Abs(inputMap[nextTile[0], nextTile[1]]) == 2) {
                    Instantiate(Path_Tile_LR, tilePosition, Quaternion.identity);
                }
                else if (Mathf.Abs(inputMap[nextTile[0], nextTile[1]]) == 3) {
                    Instantiate(Path_Tile_DL, tilePosition, Quaternion.identity);
                }
                else if (Mathf.Abs(inputMap[nextTile[0], nextTile[1]]) == 4) {
                    Instantiate(Path_Tile_UL, tilePosition, Quaternion.identity);
                }
            }
            else if (Mathf.Abs(inputMap[currentTile[0], currentTile[1]]) == 3) {
                nextTile[0]--;
                tilePosition = new Vector3(5f * nextTile[1], 0, -5f * nextTile[0]);

                if (Mathf.Abs(inputMap[nextTile[0], nextTile[1]]) == 1) {
                    Instantiate(Path_Tile_UL, tilePosition, Quaternion.identity);
                }
                else if (Mathf.Abs(inputMap[nextTile[0], nextTile[1]]) == 2) {
                    Instantiate(Path_Tile_UR, tilePosition, Quaternion.identity);
                }
                else if (Mathf.Abs(inputMap[nextTile[0], nextTile[1]]) == 3) {
                    Instantiate(Path_Tile_UD, tilePosition, Quaternion.identity);
                }
            }
            else {
                nextTile[0]++;
                tilePosition = new Vector3(5f * nextTile[1], 0, -5f * nextTile[0]);

                if (Mathf.Abs(inputMap[nextTile[0], nextTile[1]]) == 1) {
                    Instantiate(Path_Tile_DL, tilePosition, Quaternion.identity);
                }
                else if (Mathf.Abs(inputMap[nextTile[0], nextTile[1]]) == 2) {
                    Instantiate(Path_Tile_DR, tilePosition, Quaternion.identity);
                }
                else if (Mathf.Abs(inputMap[nextTile[0], nextTile[1]]) == 4) {
                    Instantiate(Path_Tile_UD, tilePosition, Quaternion.identity);
                }
            }

            currentTile[0] = nextTile[0];
            currentTile[1] = nextTile[1];
        }
        while (inputMap[currentTile[0], currentTile[1]] > 0);

        Instantiate(End_TIle, tilePosition + new Vector3(0, 0.01f, 0), Quaternion.identity);
    }
}