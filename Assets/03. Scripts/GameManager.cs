using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour //게임초기화, 레벨관리
{
    public static GameManager Instance;
    public UpgradeUI upgradeUI;
    public int stageLevel = 1;
    public int maxHP = 8;
    public int currentHP = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == (int)Escene.NormalStage)
        {
            FindObjectOfType<BoardManager>()?.SetupScene(stageLevel);
        }
        else if (scene.buildIndex == (int)Escene.BossStage)
        {
            // 보스 씬 초기화도 여기서 가능
        }

        // UI 업데이트 등도 여기서 가능
    }

    private void Start()
    {
    }

    public void OnStageCleared()
    {
        stageLevel++;
        upgradeUI.UpgradeImage();
    }
    public void ChangeStage()
    {
        if (stageLevel >= 11)  // 11 이상일 때 메인 메뉴로 이동
        {
            SceneLoader.LoadScene(Escene.MainMenu);
            return;
        }

        if (stageLevel % 5 == 0) // 5스테이지마다 보스 등장
            SceneLoader.LoadScene(Escene.BossStage);
        else
            SceneLoader.LoadScene(Escene.NormalStage);
    }


    public void GameOver()
    {
        // GameOver 처리
        SceneLoader.LoadScene(Escene.GameOver);
    }

    public void IncrementStageLevel()
    {
        OnStageCleared();
        Debug.Log("Stage Level increased to: " + stageLevel);
    }

    public void IncreaseHP(int amount = 1)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        Debug.Log($"Player healed: +{amount}, currentHP: {currentHP}");
    }

    public void TakeDamage(int amount)
    {
        currentHP = Mathf.Max(currentHP - amount, 0);
        Debug.Log($"Player took damage: -{amount}, currentHP: {currentHP}");
        // 추가로 체력 0 됐을 때 게임오버 처리 등
        if (currentHP <= 0)
            GameOver();
    }
}
