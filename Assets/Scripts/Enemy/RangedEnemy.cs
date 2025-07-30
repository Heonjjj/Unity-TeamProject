using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;
    public int projectileCount = 1; // 적 종류에 따라 달라짐 근접은 기본적으로 0

    protected override void Update()
    {
        base.Update();

        if (player != null && Time.time - lastAttackTime >= attackCooldown)
        {
            ShootProjectile();
            lastAttackTime = Time.time;
        }
    }

    private void ShootProjectile()
    {
        float angleStep = 30f; // 각도 간격
        float startAngle = -((projectileCount - 1) / 2f) * angleStep;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = startAngle + (angleStep * i);
            Vector2 dir = Quaternion.Euler(0, 0, angle) * (player.position - firePoint.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = dir * 5f;
        }
    }
}
