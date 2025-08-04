using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameHUD: MonoBehaviour
{
    public GameObject[] hearts; // 하트 오브젝트 배열 (Image 말고 GameObject)
    private int previousHP = -1;


    void Start()
    {
        UpdateHearts();
    }


    void Update()
    {
        int currentHP = GetCurrentHP();
        if (currentHP != previousHP)
        {
            UpdateHearts();
            previousHP = currentHP;
        }

        //체력 0이하, Player가 있을 때만 없으면 null이 뜨기 때문
        if (Player.Instance != null && Player.Instance.currentHP <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    void UpdateHearts()
    {
        int currentHP = GetCurrentHP();

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentHP);
        }
    }

    int GetCurrentHP()
    {
        if (Player.Instance == null) return 0;
        return (int)Player.Instance.currentHP;
    }
}
