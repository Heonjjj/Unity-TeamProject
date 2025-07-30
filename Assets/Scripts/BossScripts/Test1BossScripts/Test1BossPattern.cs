using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1BossPattern : MonoBehaviour, IBossAttackPattern
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    public float Damage { get; private set; } = 1f;

    public float Cooldown { get; private set; } = 2f;
    public void Activate()
    {
        int rand = Random.Range(0, 2);
        Damage = (rand == 0) ? 1f : 2f;

        if (rand == 0)
        {
            ShootProjectiles(1, 10f);
            Cooldown = 2f;
            Debug.Log("화살 1개 발사");
        }
        else
        {
            ShootProjectiles(5, 8f, 45f, true);
            Cooldown = 6f;
            Debug.Log("화살 5개 발사");
        }
    }

    private void ShootProjectiles(int count, float speed, float spreadAngle = 0f, bool randomCenter = false)
    {
        Vector3 dirToPlayer = GameObject.FindWithTag("Player").transform.position - firePoint.position;
        float baseAngle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;

        if (randomCenter)
            baseAngle += Random.Range(-15f, 15f);

        float angleStep = (count > 1) ? spreadAngle / (count - 1) : 0f;
        float startAngle = baseAngle - (spreadAngle / 2f);

        for (int i = 0; i < count; i++)
        {
            float angle = startAngle + (angleStep * i);
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, rotation);

            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = rotation * Vector3.right * speed;
            }

            Test1BossProjectile Script = proj.GetComponent<Test1BossProjectile>();
            if (Script != null)
            {
                Script.SetDamage(Damage);
                Script.SetRange(15f);
            }
        }
    }
}
