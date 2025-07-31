using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public BoardManager boardScript;
    private int level = 3;
    public float turnDelay = .1f;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if(Instance!=this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        boardScript = GetComponent<BoardManager>();
    }

    private void Start()
    {
        InitGame();
    }
    void InitGame()
    {
        boardScript.SetupScene(level);
    }

    public void GameOver()
    {
        enabled = false;
    }


    public void LoadBossScene()
    {
        SceneManager.LoadScene("BossScene");
    }
}
