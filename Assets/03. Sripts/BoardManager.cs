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
    private List<Vector3>gridPositions = new List<Vector3>(); //������Ʈ ����

    void InitialiseList() //����Ʈ �ʱ�ȭ
    {
        gridPositions.Clear();

        for(int x = 1; x<columns-1; x++) //��-1 ���� �������� ���ư��� Loop
        {
            for(int y = 1; y<rows-1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f)); //��ǥ����
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

    Vector3 RandomPosition() //����������Ʈ
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
        int enemyCount = (int)Mathf.Log(level, 2f); //���������� ���� ���̵�����
        //LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount); ���� ���߿� �߰�����
        LayoutObjectAtRandom(trees, treeCount.minimum, treeCount.maximum);
        LayoutObjectAtRandom(objects, objectCount.minimum, objectCount.maximum);
    }
}