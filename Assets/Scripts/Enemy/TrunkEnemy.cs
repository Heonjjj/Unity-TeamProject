using UnityEngine;

public class TrunkEnemy : RangedEnemy
{
    protected override void Update()
    {
        base.Update();

        if (animator != null)
        {
            Vector2 direction = (player.position - transform.position);

            // �̵� ���ο� ���� Idle �Ǵ� Run ��ȯ
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

        // ���� �� Attack �ִϸ��̼� ����
        if (animator != null && animator.HasParameter("Attack"))
        {
            animator.SetTrigger("Attack");
        }
    }
}
