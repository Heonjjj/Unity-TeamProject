using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharacter : Character
{
    [Header("∫∏Ω∫ Ω∫≈»")]
    [SerializeField] private float bossMaxHP = 100f;
    [SerializeField] private float bossMoveSpeed = 2f;

    protected override void Start()
    {
        base.Start();

        maxHP = bossMaxHP;
        moveSpeed = bossMoveSpeed;
    }
}
