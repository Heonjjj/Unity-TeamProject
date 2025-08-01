using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class BossBoardManager : MonoBehaviour
{
    public Tilemap floorTilemap;          // 배치 허용할 타일맵
    public Tilemap otherAllowedTilemap;   // 허용 타일맵 추가로 필요할 경우
    public Tilemap backDesignTilemap;     // 배치 제외 타일맵
    public Tilemap foreDesignTilemap;
    public Tilemap collisionTilemap;

    public GameObject[] objectPrefabs;    // 배치할 오브젝트 프리팹
    public int minObjects = 5;
    public int maxObjects = 10;

    public GameObject bossPrefab;
    private GameObject bossInstance;

    void Start()
    {
        //SpawnBoss(); // 보스 생성은 필요 시 호출
        PlaceRandomObjects();
    }

    void PlaceRandomObjects()
    {
        List<Vector3Int> validPositions = new List<Vector3Int>();

        // floor + otherAllowedTilemap에서 타일이 있는 위치만 수집
        AddValidPositionsFromTilemap(floorTilemap, validPositions);
        AddValidPositionsFromTilemap(otherAllowedTilemap, validPositions);

        // 배치할 개수 결정
        int objectCount = Random.Range(minObjects, maxObjects + 1);

        for (int i = 0; i < objectCount && validPositions.Count > 0; i++)
        {
            int randIndex = Random.Range(0, validPositions.Count);
            Vector3Int cellPos = validPositions[randIndex];
            validPositions.RemoveAt(randIndex); // 중복 방지

            // 제외 타일맵에 타일이 있으면 무시
            if (IsExcludedTile(cellPos)) continue;

            // 월드 좌표로 변환
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
        Vector3 spawnPos = new Vector3(8, 8, 0); // 위치는 상황에 맞게 조정
        bossInstance = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
    }

    public void OnBossDefeated()
    {
        Debug.Log("Boss Defeated!");
        GameManager.Instance.OnBossCleared();
    }
}