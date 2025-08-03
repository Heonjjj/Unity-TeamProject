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
