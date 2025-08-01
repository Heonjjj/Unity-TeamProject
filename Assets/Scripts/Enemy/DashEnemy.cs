using UnityEngine;
using System.Collections;

public class EnemyDash : Enemy
{
    [SerializeField] private float dashSpeed = 8f;          // ���� �ӵ�
    [SerializeField] private float dashCooldown = 2f;       // ���� ��Ÿ��
    [SerializeField] private float dashPrepareTime = 0.3f;  // ���� �غ� �ð�
    [SerializeField] private float dashRange = 5f;          // ���� �Ÿ�
    [SerializeField] private float stunDuration = 1f;       // �� �浹 �� ���� �ð�

    private bool isDashing = false;
    private bool isStunned = false;
    private bool hitWallDuringDash = false;
    private float lastDashTime = 0f;

    protected override void Update()
    {
        if (!isDashing && !isStunned)
        {
            // �÷��̾� ���� (Idle ����)
            base.Update();

            if (animator != null && animator.HasParameter("isMoving"))
            {
                animator.SetBool("isMoving", false); // Idle ����
            }

            TryDash();
        }
    }

    private void TryDash()
    {
        if (Time.time - lastDashTime >= dashCooldown)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= dashRange)
            {
                StartCoroutine(DashAttack());
                lastDashTime = Time.time;
            }
        }
    }

    private IEnumerator DashAttack()
    {
        isDashing = true;
        hitWallDuringDash = false;

        // ���� �غ� (Idle ���� + ���� ����)
        if (animator != null && animator.HasParameter("isMoving"))
        {
            animator.SetBool("isMoving", false);
        }

        FacePlayer();
        yield return new WaitForSeconds(dashPrepareTime);

        // Run �ִϸ��̼� ����
        if (animator != null && animator.HasParameter("isMoving"))
        {
            animator.SetBool("isMoving", true);
        }

        // �Ÿ� ��� ����
        Vector2 dashDirection = (player.position - transform.position).normalized;
        float traveled = 0f;

        while (traveled < dashRange && !hitWallDuringDash)
        {
            float moveStep = dashSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + dashDirection * moveStep);
            traveled += moveStep;
            yield return null;
        }

        // ���� ����
        if (!hitWallDuringDash)
        {
            if (animator != null && animator.HasParameter("isMoving"))
            {
                animator.SetBool("isMoving", false);
            }
        }

        isDashing = false;
    }

    private void FacePlayer()
    {
        if (player == null) return;

        Vector2 direction = player.position - transform.position;
        if (Mathf.Abs(direction.x) > 0.01f)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = direction.x > 0 ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            DealDamageToPlayer();
        }
        else if (isDashing && collision.collider.CompareTag("Wall"))
        {
            hitWallDuringDash = true;
            StartCoroutine(Stun());

            if (animator != null && animator.HasParameter("Hit Wall"))
            {
                animator.SetTrigger("Hit Wall");
            }
        }
    }


    private IEnumerator Stun()
    {
        isDashing = false;
        isStunned = true;

        // ���� ���� ����
        yield return new WaitForSeconds(stunDuration);

        isStunned = false;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (animator != null && animator.HasParameter("Hit"))
        {
            animator.SetTrigger("Hit");
        }
    }
}
