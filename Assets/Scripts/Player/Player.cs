using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    protected override void Start()
    {
        maxHP = 5f;
        attackPower = 10f;
        attackSpeed = 1f;
        moveSpeed = 6f;
        attackRange = 10f;                           //���� ���� ���� ����

        base.Start();                               //�ִ� ü������ �����ֱ�         
    }
}
