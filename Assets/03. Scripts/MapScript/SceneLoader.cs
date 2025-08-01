using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadMainStage()
    {
        SceneManager.LoadScene("01. NormalStage");
    }

    public static void LoadBossStage()
    {
        SceneManager.LoadScene("02. BossStage");
    }

    public static void LoadGameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
