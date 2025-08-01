using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Escene
{
    NormalStage,
    BossStage,
    MainMenu,
    GameOver,
}

public class SceneLoader : MonoBehaviour
{
    public static void LoadScene(Escene target)
    {
        SceneManager.LoadScene((int)target);
    }

}
