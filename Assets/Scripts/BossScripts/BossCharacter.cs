using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharacter : Character
{
    [Header("���� ������ (ScriptableObject")]
    [SerializeField] private BossStats bossStats;

    public string BossName => bossStats.BossName;
    public float MaxHP => bossStats.MaxHP;
    public float MoveSpeed => bossStats.MoveSpeed;

    protected override void Start()
    {
        base.Start();
        maxHP = bossStats.MaxHP;
        moveSpeed = bossStats.MoveSpeed;
    }
}
