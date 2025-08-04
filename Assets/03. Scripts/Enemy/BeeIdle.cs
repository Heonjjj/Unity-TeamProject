using UnityEngine;

public class BeeEnemy : RangedEnemy
{
    protected override void Update()
    {
        base.Update();

        // Bee는 Idle 애니메이션만 유지
        if (animator != null && animator.HasParameter("Idle"))
        {
            animator.Play("Idle");
        }
    }

    protected override void ShootProjectile()
    {
        base.ShootProjectile();

        // 공격 시 Attack 애니메이션 실행
        if (animator != null && animator.HasParameter("Attack"))
        {
            animator.SetTrigger("Attack");
        }
    }
}
