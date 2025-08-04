using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour //게임초기화, 레벨관리
{
    public static GameManager Instance;
    public UpgradeUI upgradeUI;
    public int stageLevel = 1;
    public GameObject gameManagerPrefab;
    public GameObject playerPrefab;
    private bool playerSpawned = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            stageLevel = 1;

            //플레이어 최초 1회 생성
            if (!playerSpawned)
            {
                Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                playerSpawned = true;
            }
        }
        else
        {
            Destroy(gameObject);
        }
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

            //플레이어 생성 위치 지정
            if (Player.Instance != null)
                Player.Instance.transform.position = new Vector3(7.5f, 7.5f, 0f);

            EnemySpawnManager spawnManager = FindObjectOfType<EnemySpawnManager>();
            if (spawnManager != null)
                spawnManager.StartSpawning(stageLevel);

        }
        else if (scene.buildIndex == (int)Escene.BossStage)
        {
            // 보스 씬 초기화도 여기서 가능

            //보스씬에서도 위치 지정
            if (Player.Instance != null)
                Player.Instance.transform.position = new Vector3(3.5f, 2f, 0f);
        }

        // UI 업데이트 등도 여기서 가능
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
        Destroy(gameObject);
        Destroy(Player.Instance.gameObject);
        Instantiate(gameManagerPrefab);
    }

    public void IncrementStageLevel()
    {
        OnStageCleared();
        Debug.Log("Stage Level increased to: " + stageLevel);
    }

    public void TakeDamage(int amount)
    {
        if (Player.Instance != null)
        {
            Player.Instance.TakeDamage(amount);
            Debug.Log($"Player took damage: -{amount}, currentHP: {Player.Instance.currentHP}");
            if (Player.Instance.currentHP <= 0)
                GameOver();
        }
    }

    public void IncreaseHP(int amount = 1)
    {
        if (Player.Instance != null)
        {
            Player.Instance.currentHP = Mathf.Min(Player.Instance.currentHP + amount, Player.Instance.maxHP);
            Player.Instance.RefreshStatsUI();
            Debug.Log($"Player healed: + {amount}, currentHP: {Player.Instance.currentHP}");
        }
    }
}
