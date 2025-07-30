using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharacter : Character
{
    [Header("보스 데이터 (ScriptableObject")]
    [SerializeField] private BossStats bossStats;

    public event System.Action<float> OnHPChanged;
    public event System.Action OnBossDie;

    public string BossName => bossStats.BossName;
    public float MaxHP => bossStats.MaxHP;
    public float MoveSpeed => bossStats.MoveSpeed;

    protected override void Start()
    {
        base.Start();
        maxHP = bossStats.MaxHP;
        moveSpeed = bossStats.MoveSpeed;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        OnHPChanged?.Invoke(currentHP);
    }

    protected override void Die()
    {
        Debug.Log("보스 사망!");

        OnBossDie?.Invoke();

        base.Die();
    }
}
