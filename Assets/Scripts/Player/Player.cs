using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    protected override void Start()
    {
        maxHP = 100f;
        attackPower = 10f;
        attackSpeed = 1f;
        moveSpeed = 6f;                             //���� ���� ���� ����

        base.Start();                               //�ִ� ü������ �����ֱ�         
    }
}
