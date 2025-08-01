using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossProjectilePatternBase : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform firePoint;

    protected void ShootProjectiles(int count, float speed, float spreadAngle = 0f, bool randomCenter = false, float damage = 1f)
    {
        Vector3 dirToPlayer = GameObject.FindWithTag("Player").transform.position - firePoint.position;
        float baseAngle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;

        if (randomCenter)
            baseAngle += Random.Range(-30f, 30f);

        float angleStep = (count > 1) ? spreadAngle / (count - 1) : 0f;
        float startAngle = baseAngle - (spreadAngle / 2f);

        for (int i = 0; i < count; i++)
        {
            float angle = startAngle + (angleStep * i);
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, rotation);

            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = rotation * Vector3.right * speed;

            BossProjectile script = proj.GetComponent<BossProjectile>();
            if (script != null)
            {
                script.SetDamage(damage);
                script.SetRange(15f);
            }
        }
    }
}