using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text stageText;
    public GameObject gameOverPanel;
    public GameObject clearPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void UpdateStage(int level)
    {
        if (stageText != null)
            stageText.text = "Stage " + level;
    }

    public void ShowGameOver()
    {
        gameOverPanel?.SetActive(true);
    }

    public void ShowClear()
    {
        clearPanel?.SetActive(true);
    }
}
