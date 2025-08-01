using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;
    public int projectileCount = 1;

    protected override void Update()
    {
        base.Update();

        if (player != null && Time.time - lastAttackTime >= attackCooldown)
        {
            ShootProjectile();
            lastAttackTime = Time.time;
        }
    }

    protected virtual void ShootProjectile()
    {
        float angleStep = 15f;
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
