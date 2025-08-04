using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyType
{
    public GameObject prefab;
    public float moveSpeed;
    public int health;
    public int projectileCount; // 원거리만 사용
}

public class EnemySpawnManager : MonoBehaviour
{
    public EnemyType meleeEnemy;
    public EnemyType rangedEnemy;
    public EnemyType trunkEnemy;
    public EnemyType dashEnemy;
    public EnemyType tankEnemy;

    public List<Transform> spawnPoints = new List<Transform>();

    public int enemiesPerStage = 20;
    public float spawnDelay = 0.2f;

    private int currentStage;

    public void StartSpawning(int stage)
    {
        currentStage = stage;
        StartCoroutine(InitializeAndSpawn(stage));
    }

    private IEnumerator InitializeAndSpawn(int stage)
    {
        // 보드 매니저 자동 탐색
        BoardManager boardManager = null;
        while (boardManager == null)
        {
            boardManager = FindObjectOfType<BoardManager>();
            yield return null;
        }

        // 스폰 포인트 생성
        if (spawnPoints.Count == 0)
        {
            GenerateSpawnPoints(boardManager);
        }

        yield return StartCoroutine(SpawnEnemies(stage));
    }

    public void GenerateSpawnPoints(BoardManager boardManager)
    {
        spawnPoints.Clear();

        // 맵 크기 기반으로 스폰 포인트 계산
        Vector3 boardCenter = Vector3.zero;
        int columns = boardManager.columns;
        int rows = boardManager.rows;

        float spawnDistanceX = (columns / 2f) + 1f;
        float spawnDistanceY = (rows / 2f) + 1f;

        Vector3[] directions = new Vector3[]
        {
            new Vector3(0, spawnDistanceY),    // 위
            new Vector3(0, -spawnDistanceY),   // 아래
            new Vector3(spawnDistanceX, 0),    // 오른쪽
            new Vector3(-spawnDistanceX, 0),   // 왼쪽
            new Vector3(spawnDistanceX, spawnDistanceY),     // 우상단
            new Vector3(-spawnDistanceX, spawnDistanceY),    // 좌상단
            new Vector3(spawnDistanceX, -spawnDistanceY),    // 우하단
            new Vector3(-spawnDistanceX, -spawnDistanceY)    // 좌하단
        };

        for (int i = 0; i < directions.Length; i++)
        {
            GameObject spawnPointObj = new GameObject("SpawnPoint_" + i);
            spawnPointObj.transform.position = boardCenter + directions[i];
            spawnPointObj.transform.parent = transform;
            spawnPoints.Add(spawnPointObj.transform);
        }
    }

    private IEnumerator SpawnEnemies(int stage)
    {
        int spawnCount = enemiesPerStage + (stage - 1) * 10;

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemyByStage(stage);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnEnemyByStage(int stage)
    {
        EnemyType type = SelectEnemyType(stage);
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        GameObject enemy = Instantiate(type.prefab, spawnPoint.position, Quaternion.identity);

        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.moveSpeed = type.moveSpeed;
        enemyScript.currentHP = type.health;
    }

    private EnemyType SelectEnemyType(int stage)
    {
        if (stage <= 2)
            return meleeEnemy;
        else if (stage <= 4)
            return Random.value > 0.7f ? rangedEnemy : meleeEnemy;
        else if (stage <= 6)
            return Random.value > 0.6f ? trunkEnemy : rangedEnemy;
        else if (stage <= 8)
            return Random.value > 0.5f ? dashEnemy : meleeEnemy;
        else
            return Random.value > 0.5f ? tankEnemy : trunkEnemy;
    }
}
