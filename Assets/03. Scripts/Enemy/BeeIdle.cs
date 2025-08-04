using UnityEngine;

public class BeeIdle : RangedEnemy
{
    protected override void ShootProjectile()
    {
        base.ShootProjectile();

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }
}
