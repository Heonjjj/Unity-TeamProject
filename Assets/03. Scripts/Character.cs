using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    public float attackPower;
    public float attackSpeed;
    public float moveSpeed;
    public float attackRange;

    protected virtual void Start()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void Attack(Character target)
    {
        if (target != null)
        {
            target.TakeDamage(attackPower);
        }
    }
}
