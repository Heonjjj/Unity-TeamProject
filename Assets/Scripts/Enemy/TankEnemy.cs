using UnityEngine;

public class TankEnemy : Enemy
{
    [SerializeField] private float attackCooldown = 2f;  // ���� ��Ÿ��
    [SerializeField] private float knockbackForce = 5f;  // �˹� ��

    private float lastAttackTime = 0f;

    protected override void Update()
    {
        base.Update();

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // ���� ���� �ȿ� �ְ� ��Ÿ���� �������� ����
            if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
    }

    private void AttackPlayer()
    {
        // Hit �ִϸ��̼� ����
        if (animator != null && animator.HasParameter("Hit"))
        {
            animator.SetTrigger("Hit");
        }

        // �÷��̾�� ������
        DealDamageToPlayer();

        // �÷��̾� �˹� ó��
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            Vector2 knockbackDir = (player.position - transform.position).normalized;
            playerRb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
