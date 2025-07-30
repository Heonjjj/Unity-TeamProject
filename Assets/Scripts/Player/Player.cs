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
        attackRange = 10f;                           //스탯 설정 수정 가능

        base.Start();                               //최대 체력으로 맞춰주기         
    }
}
