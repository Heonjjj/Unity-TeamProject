using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class BossBoardManager : MonoBehaviour
{
    public Tilemap floorTilemap;          // ��ġ ����� Ÿ�ϸ�
    public Tilemap otherAllowedTilemap;   // ��� Ÿ�ϸ� �߰��� �ʿ��� ���
    public Tilemap backDesignTilemap;     // ��ġ ���� Ÿ�ϸ�
    public Tilemap foreDesignTilemap;
    public Tilemap collisionTilemap;

    public GameObject[] objectPrefabs;    // ��ġ�� ������Ʈ ������
    public int minObjects = 5;
    public int maxObjects = 10;

    public GameObject bossPrefab;
    private GameObject bossInstance;

    void Start()
    {
        //SpawnBoss(); // ���� ������ �ʿ� �� ȣ��
        PlaceRandomObjects();
    }

    void PlaceRandomObjects()
    {
        List<Vector3Int> validPositions = new List<Vector3Int>();

        // floor + otherAllowedTilemap���� Ÿ���� �ִ� ��ġ�� ����
        AddValidPositionsFromTilemap(floorTilemap, validPositions);
        AddValidPositionsFromTilemap(otherAllowedTilemap, validPositions);

        // ��ġ�� ���� ����
        int objectCount = Random.Range(minObjects, maxObjects + 1);

        for (int i = 0; i < objectCount && validPositions.Count > 0; i++)
        {
            int randIndex = Random.Range(0, validPositions.Count);
            Vector3Int cellPos = validPositions[randIndex];
            validPositions.RemoveAt(randIndex); // �ߺ� ����

            // ���� Ÿ�ϸʿ� Ÿ���� ������ ����
            if (IsExcludedTile(cellPos)) continue;

            // ���� ��ǥ�� ��ȯ
            Vector3 worldPos = floorTilemap.CellToWorld(cellPos) + floorTilemap.tileAnchor;

            GameObject prefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];
            Instantiate(prefab, worldPos, Quaternion.identity);
        }
    }

    bool IsExcludedTile(Vector3Int pos)
    {
        return (backDesignTilemap != null && backDesignTilemap.HasTile(pos)) ||
               (foreDesignTilemap != null && foreDesignTilemap.HasTile(pos)) ||
               (collisionTilemap != null && collisionTilemap.HasTile(pos));
    }

    void AddValidPositionsFromTilemap(Tilemap tilemap, List<Vector3Int> posList)
    {
        if (tilemap == null) return;

        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                posList.Add(pos);
            }
        }
    }

    void SpawnBoss()
    {
        Vector3 spawnPos = new Vector3(8, 8, 0); // ��ġ�� ��Ȳ�� �°� ����
        bossInstance = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
    }

    public void OnBossDefeated()
    {
        Debug.Log("Boss Defeated!");
        GameManager.Instance.OnBossCleared();
    }
}