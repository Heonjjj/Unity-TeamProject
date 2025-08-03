using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; 

public class BoardManager : MonoBehaviour
{

    [Serializable]  //사용자정의 Attribute
    public class Count
    {
        public int minimum;
        public int maximum;
        public Count (int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }
    public int columns = 8; //맵크기
    public int rows = 8;
    public Count treeCount = new Count(10, 20); //맵 오브젝트 구현
    public Count objectCount = new Count(5, 10);
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] enemyTiles;
    public GameObject[] trees;
    public GameObject[] objects;


    private Transform boardHolder; //오브젝트들 자식으로 집어넣기
    private List<Vector2Int>gridPositions = new List<Vector2Int>(); //오브젝트 추적

    void InitialiseList() //리스트 초기화
    {
        gridPositions.Clear();

        for(int x = 1; x<columns-1; x++) //열-1 보다 작은동안 돌아가는 Loop
        {
            for(int y = 1; y<rows-1; y++)
            {
                gridPositions.Add(new Vector2Int(x, y)); //좌표저장
            }
        }
    }

    void BoardSetup() //맵생성
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x<columns+1; x++)
        {
            for(int y = -1; y<rows+1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                if ((x == -1 && y == -1)) //좌하단
                    toInstantiate = wallTiles[4];
                else if (x == columns && y == -1) //우하단
                    toInstantiate = wallTiles[5];
                else if (x == columns && y == rows) //우상단
                    toInstantiate = wallTiles[6];
                else if (x == -1 && y == rows) //좌상단
                    toInstantiate = wallTiles[7];

                else if (y == rows && x >= 0 && x < columns) //상단
                    toInstantiate = wallTiles[0];
                else if (x == columns && y >= 0 && y < rows) //오른쪽
                    toInstantiate = wallTiles[1];
                else if (y == -1 && x >= 0 && x < columns) //하단
                    toInstantiate = wallTiles[2];
                else if (x == -1 && y >= 0 && y < rows) //왼쪽
                    toInstantiate = wallTiles[3];

                GameObject instance = 
                    Instantiate(toInstantiate, new Vector3(x,y,0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    void LayoutObjectAtPositions(GameObject[] tileArray, int minimum, int maximum, List<Vector2Int> availablePositions)
    {
        int objectCount = Random.Range(minimum, maximum + 1);
        List<Vector2Int> usedPositions = new List<Vector2Int>();

        for (int i = 0; i < objectCount; i++)
        {
            int attempts = 0;
            Vector2Int randomPosition = new Vector2Int(-1, -1);
            bool positionFound = false;

            while (attempts < 100 && !positionFound && availablePositions.Count > 0)
            {
                int index = Random.Range(0, availablePositions.Count);
                randomPosition = availablePositions[index];
                availablePositions.RemoveAt(index); // 중복 방지

                positionFound = true;

                foreach (Vector2Int pos in usedPositions)
                {
                    if (Mathf.Abs(randomPosition.x - pos.x) < 3 && Mathf.Abs(randomPosition.y - pos.y) < 3)
                    {
                        positionFound = false;
                        break;
                    }
                }

                attempts++;
            }

            if (positionFound)
            {
                GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

                // 오브젝트 생성
                GameObject obj = Instantiate(tileChoice, new Vector3(randomPosition.x, randomPosition.y, 0f), Quaternion.identity);

                // 생성 직후 자동으로 RendererManager에 등록
                RendererManager.Instance?.RegisterObject(obj.transform);

                usedPositions.Add(randomPosition);
            }
        }
    }
    public void SetupScene(int level)
    {
        BoardSetup();
        InitialiseList();

        int enemyCount = (int)Mathf.Log(level, 2f);

        // 좌표 영역 분할
        List<Vector2Int> treePositions = new List<Vector2Int>();
        List<Vector2Int> objectPositions = new List<Vector2Int>();
        List<Vector2Int> remaining = new List<Vector2Int>();

        foreach (Vector2Int pos in gridPositions)
        {
            if (pos.x <= 1 || pos.x >= columns-2 || pos.y <= 1 || pos.y >= rows-2)
                treePositions.Add(pos);
            else if (pos.x >= 4 && pos.x <= 11 && pos.y >= 4 && pos.y <= 11)
                objectPositions.Add(pos);
            else
                remaining.Add(pos);
        }

        // 원본 갱신 (필요시)
        gridPositions = remaining;

        LayoutObjectAtPositions(trees, treeCount.minimum, treeCount.maximum, treePositions);
        LayoutObjectAtPositions(objects, objectCount.minimum, objectCount.maximum, objectPositions);
    }
}