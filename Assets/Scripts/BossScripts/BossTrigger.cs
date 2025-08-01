using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("UI")]
    [SerializeField] private GameObject bossHPBar;
    private Slider hpSlider;

    private BossCharacter bossCharacter;

    private void Start()
    {
        int currentStage = GetCurrentStage(); // ���� GameManager ����
        BossStageData selectedBoss = bossStages.Find(b => b.stage == currentStage);

        if (selectedBoss == null || selectedBoss.bossPrefab == null)
        {
            gameObject.SetActive(false); // �ش� ���������� ���� ����
            return;
        }

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
        bossCharacter.OnHPChanged += UpdateHPBar;
        bossCharacter.OnBossDie += OnBossDead;

        bossHPBar.SetActive(true);
        hpSlider = bossHPBar.GetComponentInChildren<Slider>();

        StartCoroutine(InitHPBar());
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

    private IEnumerator InitHPBar()
    {
        yield return null;
        hpSlider.maxValue = bossCharacter.MaxHP;
        hpSlider.value = bossCharacter.currentHP;
    }

    private void UpdateHPBar(float currentHP)
    {
        hpSlider.value = currentHP;
    }

    private void OnBossDead()
    {
        bossHPBar.gameObject.SetActive(false);
        // �� ��ȯ, Ŭ���� �� �߰�
    }

    private void OnDestroy()
    {
        if (bossCharacter != null)
        {
            bossCharacter.OnHPChanged -= UpdateHPBar;
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

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.B) && bossCharacter == null)
        {
            Debug.Log("�׽�Ʈ: ���� ���� ��ȯ �õ�");
            BossStageData selectedBoss = bossStages.Find(b => b.stage == GetCurrentStage());

            if (selectedBoss != null && selectedBoss.bossPrefab != null)
            {
                SpawnBoss(selectedBoss.bossPrefab);
            }
        }

        if (Input.GetKeyDown(KeyCode.N) && bossCharacter != null)
        {
            bossCharacter.SetHP(1);
            Debug.Log("���� ü�� 1�� ������");
        }

        if (Input.GetKeyDown(KeyCode.M) && bossCharacter != null)
        {
            bossCharacter.TakeDamage(99999); // ���� ���
            Debug.Log("���� ��� ó����");
        }
#endif
    }
}