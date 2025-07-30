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
    private List<Vector3>gridPositions = new List<Vector3>(); //오브젝트 추적

    void InitialiseList() //리스트 초기화
    {
        gridPositions.Clear();

        for(int x = 1; x<columns-1; x++) //열-1 보다 작은동안 돌아가는 Loop
        {
            for(int y = 1; y<rows-1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f)); //좌표저장
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

    Vector3 RandomPosition() //랜덤오브젝트
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum+1);

        for(int i = 0; i< objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitialiseList();
        //LayoutObjectAtRandom(floorTiles, objectCount.minimum, objectCount.maximum);
        int enemyCount = (int)Mathf.Log(level, 2f); //레벨증가에 따른 난이도증가
        //LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount); 몬스터 나중에 추가구현
        LayoutObjectAtRandom(trees, treeCount.minimum, treeCount.maximum);
        LayoutObjectAtRandom(objects, objectCount.minimum, objectCount.maximum);
    }
}