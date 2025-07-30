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
    [Header("보스 목록 (스테이지별)")]
    [SerializeField] private List<BossStageData> bossStages;

    [Header("스폰 위치")]
    [SerializeField] private Vector2 spawnMin = new Vector2(-8f, -4f);
    [SerializeField] private Vector2 spawnMax = new Vector2(8f, 4f);

    [Header("UI")]
    [SerializeField] private Slider hpSlider;

    private BossCharacter bossCharacter;

    private void Start()
    {
        int currentStage = GetCurrentStage(); // 추후 GameManager 연동
        BossStageData selectedBoss = bossStages.Find(b => b.stage == currentStage);

        if (selectedBoss == null || selectedBoss.bossPrefab == null)
        {
            gameObject.SetActive(false); // 해당 스테이지에 보스 없음
            return;
        }

        SpawnBoss(selectedBoss.bossPrefab);
    }

    private int GetCurrentStage()
    {
        // 나중에 GameManager로 대체
        return 5; // 테스트용
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
        // 씬 전환, 클리어 등 추가
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