using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1BossProjectile : MonoBehaviour
{
    private float damage;
    private float maxRange;
    private Vector3 spawnPosition;

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetRange(float range)
    {
        maxRange = range;
        spawnPosition = transform.position;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, spawnPosition);
        if (distance >= maxRange)
        {
            Destroy(gameObject);
        }
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

        else if (other.CompareTag("Wall") || other.CompareTag("Obstacle")) // 벽, 장애물과 충돌 시 제거
        {
            Destroy(gameObject);
        }
    }
}
