using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameHUD: MonoBehaviour
{
    public GameObject[] hearts; // ��Ʈ ������Ʈ �迭 (Image ���� GameObject)
    private int previousHP = -1;


    void Start()
    {
        UpdateHearts();
    }


    void Update()
    {
        int currentHP = GameManager.Instance.currentHP;
        if (currentHP != previousHP)
        {
            UpdateHearts();
            previousHP = currentHP;
        }

        if (currentHP <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    void UpdateHearts()
    {
        int currentHP = GameManager.Instance.currentHP;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentHP);
        }
    }
}
