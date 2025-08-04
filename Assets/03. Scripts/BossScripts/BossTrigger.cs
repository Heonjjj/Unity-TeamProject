using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossStageData
{
    public int stage;
    public GameObject bossPrefab;
}

public class BossTrigger : MonoBehaviour
{
    [Header("���� ��� (����������)")]
    [SerializeField] private List<BossStageData> bossStages;

    [Header("���� ��ġ")]
    [SerializeField] private Vector2 spawnMin = new Vector2(-8f, -4f);
    [SerializeField] private Vector2 spawnMax = new Vector2(8f, 4f);

    private BossCharacter bossCharacter;

    private static BossTrigger instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        int currentStage = GetCurrentStage();
        Debug.Log($"[BossTrigger] ���� ��������: {currentStage}");
        BossStageData selectedBoss = bossStages.Find(b => b.stage == currentStage);

        foreach (var data in bossStages)
        {
            Debug.Log($"[BossTrigger] ��ϵ� ���� - Stage: {data.stage}, Prefab: {data.bossPrefab?.name}");
        }

        if (selectedBoss == null || selectedBoss.bossPrefab == null)
        {
            Debug.LogWarning("[BossTrigger] �ش� ���������� ������ ���ų� �������� �����ϴ�.");
            return;
        }

        Debug.Log("[BossTrigger] ���� ��ȯ ����");
        SpawnBoss(selectedBoss.bossPrefab);
    }

    private int GetCurrentStage()
    {
        return GameManager.Instance.stageLevel;
    }

    private void SpawnBoss(GameObject bossPrefab)
    {
        Vector3 spawnPos = GetSafeSpawnPosition();
        GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);

        bossCharacter = boss.GetComponent<BossCharacter>();

        bossCharacter.OnBossDie += OnBossDead;
    }

    private Vector3 GetSafeSpawnPosition()
    {
        const int maxAttempts = 20;
        float safeRadius = 3f;

        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerPos = player != null ? player.transform.position : Vector3.zero;

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 candidate = new Vector3(
                Random.Range(spawnMin.x, spawnMax.x),
                Random.Range(spawnMin.y, spawnMax.y),
                0f
            );

            if (Vector3.Distance(candidate, playerPos) > safeRadius)
            {
                return candidate;
            }
        }

        Debug.LogWarning("���� �Ÿ� �� ���� ��ġ�� ã�� ���� ������ ������ ��ġ ���");
        return new Vector3(
            Random.Range(spawnMin.x, spawnMax.x),
            Random.Range(spawnMin.y, spawnMax.y),
            0f
        );
    }

    private void OnBossDead()
    {

    }

    private void OnDestroy()
    {
        if (bossCharacter != null)
        {
            bossCharacter.OnBossDie -= OnBossDead;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 center = new Vector3(
            (spawnMin.x + spawnMax.x) / 2f,
            (spawnMin.y + spawnMax.y) / 2f,
            0f
        );
        Vector3 size = new Vector3(
            Mathf.Abs(spawnMax.x - spawnMin.x),
            Mathf.Abs(spawnMax.y - spawnMin.y),
            0f
        );

        Gizmos.DrawWireCube(center, size);
    }
}