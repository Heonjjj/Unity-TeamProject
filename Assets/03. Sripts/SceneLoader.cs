using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadMainStage()
    {
        SceneManager.LoadScene("MainStage");
    }

    public static void LoadBossStage()
    {
        SceneManager.LoadScene("BossStage");
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
