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
        int currentHP = GetCurrentHP();
        if (currentHP != previousHP)
        {
            UpdateHearts();
            previousHP = currentHP;
        }

        //ü�� 0����, Player�� ���� ���� ������ null�� �߱� ����
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
