using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossBoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;
        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 12;
    public int rows = 12;

    public Count obstacleCount = new Count(15, 25);
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] obstacleTiles;

    public GameObject bossPrefab;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("BossBoard").transform;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                // 벽 배치 (모서리 및 테두리)
                if ((x == -1 && y == -1))
                    toInstantiate = wallTiles[4];
                else if (x == columns && y == -1)
                    toInstantiate = wallTiles[5];
                else if (x == columns && y == rows)
                    toInstantiate = wallTiles[6];
                else if (x == -1 && y == rows)
                    toInstantiate = wallTiles[7];
                else if (y == rows && x >= 0 && x < columns)
                    toInstantiate = wallTiles[0];
                else if (x == columns && y >= 0 && y < rows)
                    toInstantiate = wallTiles[1];
                else if (y == -1 && x >= 0 && x < columns)
                    toInstantiate = wallTiles[2];
                else if (x == -1 && y >= 0 && y < rows)
                    toInstantiate = wallTiles[3];

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupBossScene()
    {
        BoardSetup();
        InitialiseList();
        LayoutObjectAtRandom(obstacleTiles, obstacleCount.minimum, obstacleCount.maximum);

        // 보스는 중앙 근처에 배치 (예: 맵 중앙)
        Vector3 bossPosition = new Vector3(columns / 2, rows / 2, 0f);
        Instantiate(bossPrefab, bossPosition, Quaternion.identity);
    }
}
