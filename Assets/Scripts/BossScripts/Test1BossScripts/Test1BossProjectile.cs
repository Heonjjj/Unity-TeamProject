using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1BossProjectile : MonoBehaviour
{
    private float damage;

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Character>(); // <나중에 PlayerCharacter로 변경
            if (player != null) // 플레이어에게 발사체의 damage 수치만큼 데미지
            {
                player.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
