using UnityEngine;

public class Enemy : Character
{
    protected Transform player;
    protected Animator animator;
    protected Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (player == null) return;
        MoveTowardsPlayer();
        HandleDirection();
    }

    protected virtual void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        if (animator != null && animator.HasParameter("isMoving"))
            animator.SetBool("isMoving", direction.magnitude > 0.1f);
    }

    private void HandleDirection()
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

    public virtual void DealDamageToPlayer()
    {
        if (player != null)
        {
            Character playerCharacter = player.GetComponent<Character>();
            if (playerCharacter != null)
                playerCharacter.TakeDamage(attackPower);
        }
    }
}
