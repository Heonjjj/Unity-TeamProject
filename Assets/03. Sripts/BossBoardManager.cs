using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class BossBoardManager : MonoBehaviour
{
    public Tilemap floorTilemap; // floor tilemap만 체크함
    public GameObject[] objectPrefabs; // 랜덤으로 배치할 오브젝트 프리팹들
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

        // 1. floor에만 타일이 있는 위치 수집
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (floorTilemap.HasTile(pos))
            {
                validPositions.Add(pos);
            }
        }

        // 2. 랜덤으로 몇 개 배치할지 결정
        int objectCount = Random.Range(minObjects, maxObjects + 1);

        for (int i = 0; i < objectCount && validPositions.Count > 0; i++)
        {
            int randIndex = Random.Range(0, validPositions.Count);
            Vector3Int cellPos = validPositions[randIndex];
            validPositions.RemoveAt(randIndex); // 중복 방지

            // 3. 프리팹 선택 + 월드 위치로 변환
            GameObject prefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];
            Vector3 worldPos = floorTilemap.GetCellCenterWorld(cellPos);

            Instantiate(prefab, worldPos, Quaternion.identity);
        }
    }

    void SpawnBoss()
    {
        // 보스 생성 위치는 보스맵에 맞게 수정하세요
        Vector3 spawnPos = new Vector3(8, 8, 0);
        bossInstance = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
    }

    public void OnBossDefeated()
    {
        Debug.Log("Boss Defeated!");
        GameManager.Instance.OnBossCleared();
    }
}
