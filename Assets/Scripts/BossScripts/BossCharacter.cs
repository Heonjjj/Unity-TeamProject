using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharacter : Character
{
    [Header("���� ������ (ScriptableObject")]
    [SerializeField] private BossStats bossStats;

    public event System.Action<float> OnHPChanged;
    public event System.Action OnBossDie;

    public string BossName => bossStats.BossName;
    public float MaxHP => bossStats.MaxHP;
    public float MoveSpeed => bossStats.MoveSpeed;

    protected void Awake()
    {
        if (bossStats == null)
        {
            Debug.LogError("BossStats�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        maxHP = bossStats.MaxHP;
        moveSpeed = bossStats.MoveSpeed;
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        OnHPChanged?.Invoke(currentHP);
    }

    protected override void Die()
    {
        Debug.Log("���� ���!");

        OnBossDie?.Invoke();

        base.Die();
    }
}
