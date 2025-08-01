using UnityEngine;

public class TankEnemy : Enemy
{
    [SerializeField] private float attackCooldown = 2f;  // 공격 쿨타임
    [SerializeField] private float knockbackForce = 5f;  // 넉백 힘

    private float lastAttackTime = 0f;

    protected override void Update()
    {
        base.Update();

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // 공격 범위 안에 있고 쿨타임이 끝났으면 공격
            if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
    }

    private void AttackPlayer()
    {
        // Hit 애니메이션 실행
        if (animator != null && animator.HasParameter("Hit"))
        {
            animator.SetTrigger("Hit");
        }

        // 플레이어에게 데미지
        DealDamageToPlayer();

        // 플레이어 넉백 처리
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            Vector2 knockbackDir = (player.position - transform.position).normalized;
            playerRb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
