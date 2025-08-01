using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private float damage;
    private float maxRange;
    private Vector3 spawnPosition;

    private bool isDestroyed = false;

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
            PrepareDestroy();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestroyed) return;

        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            PrepareDestroy();
        }
    }

    private void PrepareDestroy()
    {
        if (isDestroyed) return;
        isDestroyed = true;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null) renderer.enabled = false;

        Destroy(gameObject, 0.05f);
    }
}
