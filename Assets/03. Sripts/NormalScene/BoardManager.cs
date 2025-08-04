using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; 

public class BoardManager : MonoBehaviour
{

    [Serializable]  //��������� Attribute
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
    public int columns = 8; //��ũ��
    public int rows = 8;
    public Count treeCount = new Count(10, 20); //�� ������Ʈ ����
    public Count objectCount = new Count(5, 10);
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] enemyTiles;
    public GameObject[] trees;
    public GameObject[] objects;


    private Transform boardHolder; //������Ʈ�� �ڽ����� ����ֱ�
    private List<Vector2Int>gridPositions = new List<Vector2Int>(); //������Ʈ ����

    void InitialiseList() //����Ʈ �ʱ�ȭ
    {
        gridPositions.Clear();

        for(int x = 1; x<columns-1; x++) //��-1 ���� �������� ���ư��� Loop
        {
            for(int y = 1; y<rows-1; y++)
            {
                gridPositions.Add(new Vector2Int(x, y)); //��ǥ����
            }
        }
    }

    void BoardSetup() //�ʻ���
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x<columns+1; x++)
        {
            for(int y = -1; y<rows+1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                if ((x == -1 && y == -1)) //���ϴ�
                    toInstantiate = wallTiles[4];
                else if (x == columns && y == -1) //���ϴ�
                    toInstantiate = wallTiles[5];
                else if (x == columns && y == rows) //����
                    toInstantiate = wallTiles[6];
                else if (x == -1 && y == rows) //�»��
                    toInstantiate = wallTiles[7];

                else if (y == rows && x >= 0 && x < columns) //���
                    toInstantiate = wallTiles[0];
                else if (x == columns && y >= 0 && y < rows) //������
                    toInstantiate = wallTiles[1];
                else if (y == -1 && x >= 0 && x < columns) //�ϴ�
                    toInstantiate = wallTiles[2];
                else if (x == -1 && y >= 0 && y < rows) //����
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
                availablePositions.RemoveAt(index); // �ߺ� ����

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

                // ������Ʈ ����
                GameObject obj = Instantiate(tileChoice, new Vector3(randomPosition.x, randomPosition.y, 0f), Quaternion.identity);

                // ���� ���� �ڵ����� RendererManager�� ���
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

        // ��ǥ ���� ����
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

        // ���� ���� (�ʿ��)
        gridPositions = remaining;

        LayoutObjectAtPositions(trees, treeCount.minimum, treeCount.maximum, treePositions);
        LayoutObjectAtPositions(objects, objectCount.minimum, objectCount.maximum, objectPositions);
    }
}