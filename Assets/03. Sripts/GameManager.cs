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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == (int)Escene.NormalStage)
        {
            FindObjectOfType<BoardManager>()?.SetupScene(stageLevel);
        }
        else if (currentSceneIndex == (int)Escene.BossStage)
        {
        }

        //UIManager.Instance?.UpdateStage(stageLevel);
    }

    public void OnStageCleared()
    {
        stageLevel++;
        if (stageLevel % 5 == 0) //5스테이지마다 보스 등장
            SceneLoader.LoadScene(Escene.BossStage);
        else
        {
            SceneLoader.LoadScene(Escene.NormalStage);
        }
    }

    public void OnBossCleared()
    {
        SceneLoader.LoadScene(Escene.MainMenu);
    }

    public void GameOver()
    {
        // GameOver 처리
        SceneLoader.LoadScene(Escene.GameOver);
    }
}
