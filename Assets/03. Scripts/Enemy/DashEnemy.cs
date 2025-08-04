using UnityEngine;
using System.Collections;

public class EnemyDash : Enemy
{
    [SerializeField] private float dashSpeed = 8f;          // 돌진 속도
    [SerializeField] private float dashCooldown = 2f;       // 돌진 쿨타임
    [SerializeField] private float dashPrepareTime = 0.3f;  // 돌진 준비 시간
    [SerializeField] private float dashRange = 5f;          // 돌진 거리
    [SerializeField] private float stunDuration = 1f;       // 벽 충돌 시 기절 시간

    private bool isDashing = false;
    private bool isStunned = false;
    private bool hitWallDuringDash = false;
    private float lastDashTime = 0f;

    protected override void Update()
    {
        if (!isDashing && !isStunned)
        {
            // 플레이어 추적 (Idle 유지)
            base.Update();

            if (animator != null && animator.HasParameter("isMoving"))
            {
                animator.SetBool("isMoving", false); // Idle 유지
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

        // 돌진 준비 (Idle 정지 + 방향 고정)
        if (animator != null && animator.HasParameter("isMoving"))
        {
            animator.SetBool("isMoving", false);
        }

        FacePlayer();
        yield return new WaitForSeconds(dashPrepareTime);

        // Run 애니메이션 실행
        if (animator != null && animator.HasParameter("isMoving"))
        {
            animator.SetBool("isMoving", true);
        }

        // 거리 기반 돌진
        Vector2 dashDirection = (player.position - transform.position).normalized;
        float traveled = 0f;

        while (traveled < dashRange && !hitWallDuringDash)
        {
            float moveStep = dashSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + dashDirection * moveStep);
            traveled += moveStep;
            yield return null;
        }

        // 돌진 종료
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

        // 기절 상태 유지
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
