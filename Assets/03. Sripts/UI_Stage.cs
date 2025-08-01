using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stage: MonoBehaviour
{
    GameManager gameManager;
    public GameObject[] hearts; // ��Ʈ ������Ʈ �迭 (Image ���� GameObject)

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
            // ���ʺ��� ���ʷ� ��Ȱ��ȭ
            hearts[i].SetActive(i < currentHP);
        }
    }
}
