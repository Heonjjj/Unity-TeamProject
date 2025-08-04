using UnityEngine;

public class Enemy : Character
{
    protected Transform player;
    protected Animator animator;
    protected Rigidbody2D rb;

    [SerializeField] protected float attackCooldown = 1f;
    protected float lastAttackTime = 0f;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        MoveTowardsPlayer();
    }

    protected void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        if (animator != null)
            animator.SetBool("isMoving", direction.magnitude > 0.1f);
    }

    protected virtual void ShootProjectile()
    {
        // 원거리 적은 오버라이드에서 구현
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DealDamageToPlayer(collision);
        }
    }

    protected void DealDamageToPlayer(Collider2D collision)
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Character playerCharacter = collision.GetComponent<Character>();
            if (playerCharacter != null)
            {
                playerCharacter.TakeDamage(attackPower);
                lastAttackTime = Time.time;
                Debug.Log($"Player hit by {gameObject.name}, Damage: {attackPower}");
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (animator != null && animator.HasParameter("Hit"))
        {
            animator.SetTrigger("Hit");
        }
    }

    protected override void Die()
    {
        if (animator != null && animator.HasParameter("Die"))
        {
            animator.SetTrigger("Die");
            Destroy(gameObject, 0.5f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
