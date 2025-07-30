using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonsterTest : Character
{
    protected override void Start()
    {
        maxHP = 10f;
        attackPower = 1f;
        moveSpeed = 2f;

        base.Start();
    }
}
