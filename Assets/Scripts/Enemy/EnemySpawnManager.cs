using System.Collections;
using UnityEngine;

[System.Serializable]
public class EnemyType 
{
    public GameObject prefab;
    public float moveSpeed;
    public int health;
    public int projectileCount; // 근접이면 0
}

public class EnemySpawnManager : MonoBehaviour
{
    public EnemyType[] enemyTypes;        // 여러 적 타입 관리
    public Transform[] spawnPoints;       // 스폰 위치 배열
    public int enemiesPerStage = 20;      // 기본 20마리씩 증가
    public float spawnDelay = 0.2f;       // 스폰 간격

    //public void Start() // 테스트용 몬스터 스폰 사용하실땐 주석만 제거하기
    //{
    //    StartSpawning(2);
    //}

    public void StartSpawning(int stage)
    {
        StartCoroutine(SpawnEnemies(stage));
    }

    private IEnumerator SpawnEnemies(int stage)
    {
        int spawnCount = enemiesPerStage * stage;

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnEnemy()
    {
        EnemyType type = enemyTypes[Random.Range(0, enemyTypes.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(type.prefab, spawnPoint.position, Quaternion.identity);

        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.moveSpeed = type.moveSpeed;
        enemyScript.health = type.health;

        if (enemyScript is RangedEnemy ranged)
        {
            ranged.projectileCount = type.projectileCount;
        }
    }
}
