using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stage: MonoBehaviour
{
    GameManager gameManager;
    public GameObject[] hearts; // 하트 오브젝트 배열 (Image 말고 GameObject)

    [Range(0, 5)]
    public int currentHP = 5;
    private int previousHP = -1;

    void Update()
    {
        if (currentHP != previousHP)
        {
            UpdateHearts();
            previousHP = currentHP;
        }

        if (currentHP <= 0)
            gameManager.GameOver();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            // 왼쪽부터 차례로 비활성화
            hearts[i].SetActive(i < currentHP);
        }
    }
}
