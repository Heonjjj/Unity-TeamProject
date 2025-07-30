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
    [SerializeField] private Slider hpSlider;

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
        // ���߿� GameManager�� ��ü
        return 5; // �׽�Ʈ��
    }

    private void SpawnBoss(GameObject bossPrefab)
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(spawnMin.x, spawnMax.x),
            Random.Range(spawnMax.y, spawnMin.y),
            0f
        );

        GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);

        bossCharacter = boss.GetComponent<BossCharacter>();
        bossCharacter.OnHPChanged += UpdateHPBar;
        bossCharacter.OnBossDie += OnBossDead;

        hpSlider.maxValue = bossCharacter.MaxHP;
        hpSlider.value = bossCharacter.currentHP;
    }

    private void UpdateHPBar(float currentHP)
    {
        hpSlider.value = currentHP;
    }

    private void OnBossDead()
    {
        hpSlider.gameObject.SetActive(false);
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
}