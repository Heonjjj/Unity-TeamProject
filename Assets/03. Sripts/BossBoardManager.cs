using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class BossBoardManager : MonoBehaviour
{
    public Tilemap floorTilemap; // floor tilemap�� üũ��
    public GameObject[] objectPrefabs; // �������� ��ġ�� ������Ʈ �����յ�
    public int minObjects = 5;
    public int maxObjects = 10;
    public GameObject bossPrefab;
    private GameObject bossInstance;

    void Start()
    {
        //SpawnBoss();
        PlaceRandomObjects();
    }

    void PlaceRandomObjects()
    {
        BoundsInt bounds = floorTilemap.cellBounds;
        List<Vector3Int> validPositions = new List<Vector3Int>();

        // 1. floor���� Ÿ���� �ִ� ��ġ ����
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (floorTilemap.HasTile(pos))
            {
                validPositions.Add(pos);
            }
        }

        // 2. �������� �� �� ��ġ���� ����
        int objectCount = Random.Range(minObjects, maxObjects + 1);

        for (int i = 0; i < objectCount && validPositions.Count > 0; i++)
        {
            int randIndex = Random.Range(0, validPositions.Count);
            Vector3Int cellPos = validPositions[randIndex];
            validPositions.RemoveAt(randIndex); // �ߺ� ����

            // 3. ������ ���� + ���� ��ġ�� ��ȯ
            GameObject prefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];
            Vector3 worldPos = floorTilemap.GetCellCenterWorld(cellPos);

            Instantiate(prefab, worldPos, Quaternion.identity);
        }
    }

    void SpawnBoss()
    {
        // ���� ���� ��ġ�� �����ʿ� �°� �����ϼ���
        Vector3 spawnPos = new Vector3(8, 8, 0);
        bossInstance = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
    }

    public void OnBossDefeated()
    {
        Debug.Log("Boss Defeated!");
        GameManager.Instance.OnBossCleared();
    }
}
