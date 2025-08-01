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
        //UIManager.Instance.UpdateStage(GameManager.Instance.stageLevel);
        //UIManager.Instance.ShowClear();

        if (SceneManager.GetActiveScene().buildIndex == (int)Escene.NormalStage)
        {
            FindObjectOfType<BoardManager>()?.SetupScene(stageLevel);
        }
    }

    public void OnStageCleared()
    {
        if (stageLevel % 5 == 0) //5스테이지마다 보스 등장
            SceneManager.LoadScene("02. BossStage");
        else
        {
            stageLevel++;
            SceneManager.LoadScene("01. NormalStage");
        }
    }

    public void OnBossCleared()
    {
        stageLevel++;
        SceneManager.LoadScene("03. MainMenu");
    }

    public void GameOver()
    {
        // GameOver 처리
        SceneManager.LoadScene("04. GameOver");
    }
}
