using System.Collections;
using UnityEngine;

[System.Serializable]
public class EnemyType 
{
    public GameObject prefab;
    public float moveSpeed;
    public int health;
    public int projectileCount; // �����̸� 0
}

public class EnemySpawnManager : MonoBehaviour
{
    public EnemyType[] enemyTypes;        // ���� �� Ÿ�� ����
    public Transform[] spawnPoints;       // ���� ��ġ �迭
    public int enemiesPerStage = 20;      // �⺻ 20������ ����
    public float spawnDelay = 0.2f;       // ���� ����

    //public void Start() // �׽�Ʈ�� ���� ���� ����ϽǶ� �ּ��� �����ϱ�
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
