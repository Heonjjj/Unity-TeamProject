using UnityEngine;

public class TrunkEnemy : RangedEnemy
{
    protected override void Update()
    {
        base.Update();

        if (animator != null)
        {
            Vector2 direction = (player.position - transform.position);

            // 이동 여부에 따라 Idle 또는 Run 전환
            if (animator.HasParameter("isMoving"))
            {
                bool isMoving = direction.magnitude > 0.1f;
                animator.SetBool("isMoving", isMoving);
            }
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
