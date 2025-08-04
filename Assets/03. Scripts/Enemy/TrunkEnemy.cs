using UnityEngine;

public class TrunkEnemy : RangedEnemy
{
    [SerializeField] private float attackDelay = 0.5f;

    protected override void ShootProjectile()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        Invoke(nameof(FireProjectile), attackDelay);
    }

    private void FireProjectile()
    {
        base.ShootProjectile();
    }
}
