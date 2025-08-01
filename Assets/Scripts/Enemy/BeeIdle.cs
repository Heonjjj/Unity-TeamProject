using UnityEngine;

public class BeeEnemy : RangedEnemy
{
    protected override void Update()
    {
        base.Update();

        // Bee�� Idle �ִϸ��̼Ǹ� ����
        if (animator != null && animator.HasParameter("Idle"))
        {
            animator.Play("Idle");
        }
    }

    protected override void ShootProjectile()
    {
        base.ShootProjectile();

        // ���� �� Attack �ִϸ��̼� ����
        if (animator != null && animator.HasParameter("Attack"))
        {
            animator.SetTrigger("Attack");
        }
    }
}
