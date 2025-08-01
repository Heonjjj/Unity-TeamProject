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
    [SerializeField] private GameObject bossHPBar;
    private Slider hpSlider;

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

        bossHPBar.SetActive(true);
        hpSlider = bossHPBar.GetComponentInChildren<Slider>();

        StartCoroutine(InitHPBar());
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

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.B) && bossCharacter == null)
        {
            Debug.Log("테스트: 강제 보스 소환 시도");
            BossStageData selectedBoss = bossStages.Find(b => b.stage == GetCurrentStage());

            if (selectedBoss != null && selectedBoss.bossPrefab != null)
            {
                SpawnBoss(selectedBoss.bossPrefab);
            }
        }

        if (Input.GetKeyDown(KeyCode.N) && bossCharacter != null)
        {
            bossCharacter.SetHP(1);
            Debug.Log("보스 체력 1로 설정됨");
        }

        if (Input.GetKeyDown(KeyCode.M) && bossCharacter != null)
        {
            bossCharacter.TakeDamage(99999); // 강제 즉사
            Debug.Log("보스 즉사 처리됨");
        }
#endif
    }
}