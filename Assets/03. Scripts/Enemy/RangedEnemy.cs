using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileCooldown = 2f;
    private float lastProjectileTime = 0f;

    protected override void Update()
    {
        base.Update();

        if (player != null && Time.time - lastProjectileTime >= projectileCooldown)
        {
            ShootProjectile();
            lastProjectileTime = Time.time;
        }
    }

    protected override void ShootProjectile()
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = (player.position - transform.position).normalized;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 5f;
    }
}
