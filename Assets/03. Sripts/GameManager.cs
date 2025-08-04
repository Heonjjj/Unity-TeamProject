using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour //게임초기화, 레벨관리
{
    public static GameManager Instance;

    public int stageLevel = 1;

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

    private void Start()
    {
        UIManager.Instance.UpdateStage(GameManager.Instance.stageLevel);
        UIManager.Instance.ShowClear();

        if (SceneManager.GetActiveScene().name == "MainStage")
        {
            BoardManager boardManager = FindObjectOfType<BoardManager>();
            boardManager?.SetupScene(stageLevel);

            EnemySpawnManager spawnManager = FindObjectOfType<EnemySpawnManager>();
            if (spawnManager != null)
                spawnManager.StartSpawning(stageLevel);
        }
<<<<<<< Updated upstream
=======
        else if (scene.buildIndex == (int)Escene.BossStage)
        {
            // 보스 씬 초기화도 여기서 가능
        }

        // UI 업데이트 등도 여기서 가능
>>>>>>> Stashed changes
    }

    public void OnStageCleared()
    {
        if (stageLevel % 5 == 0) // 예: 3스테이지마다 보스 등장
            SceneManager.LoadScene("BossStage");
        else
        {
            stageLevel++;
            SceneManager.LoadScene("MainStage");
        }
    }

    public void OnBossCleared()
    {
        stageLevel++;
        SceneManager.LoadScene("MainStage");
    }

    public void GameOver()
    {
        // GameOver 처리
        SceneManager.LoadScene("GameOverScene");
    }
}
