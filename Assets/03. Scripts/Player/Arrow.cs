using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Arrow : MonoBehaviour
{
    private Character owner;
    private bool hasHit = false;

    public void SetOwner(Character attacker)
    {
        owner = attacker;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            hasHit = true;
            Destroy(gameObject);
            return;
        }

        if (other.GetComponent<TilemapCollider2D>() != null)
        {
            hasHit = true;
            Destroy(gameObject);
            return;
        }

        BossCharacter boss = other.GetComponentInParent<BossCharacter>();

        if (boss != null && boss != owner)
        {
            boss.TakeDamage(owner.attackPower);
            hasHit = true;
            Destroy(gameObject);
            return;
        }

        Character enemy = other.GetComponentInParent<Character>();
        if (enemy != null && enemy != owner && !(enemy is BossCharacter))
        {
            enemy.TakeDamage(owner.attackPower);
            hasHit = true;
            Destroy(gameObject);
            return;
        }
    }
}
