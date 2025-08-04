using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Arrow : MonoBehaviour
{
    private Character owner;

    public void SetOwner(Character attacker)
    {
        owner = attacker;
        Destroy(gameObject, 0.8f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BossCharacter boss = other.GetComponentInParent<BossCharacter>();

        if (boss != null && boss != owner)
        {
            boss.TakeDamage(owner.attackPower);
            Destroy(gameObject);
            return;
        }

        Character enemy = other.GetComponentInParent<Character>();
        if (enemy != null && enemy != owner && !(enemy is BossCharacter))
        {
            enemy.TakeDamage(owner.attackPower);
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Wall") || other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            return;
        }

        if (other.GetComponent<TilemapCollider2D>() != null)
        {
            Destroy(gameObject);
            return;
        }
    }
}
