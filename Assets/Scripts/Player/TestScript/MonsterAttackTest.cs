using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackTest : MonoBehaviour
{
    private Character selfCharacter;

    private void Awake()
    {
        selfCharacter = GetComponent<Character>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character target = collision.gameObject.GetComponent<Character>();
        if (target != null && target is Player)
        {
            target.TakeDamage(selfCharacter.attackPower);
            Debug.Log("플레이어가 몬스터와 닿아서 데미지 받음");
        }
    }
}
