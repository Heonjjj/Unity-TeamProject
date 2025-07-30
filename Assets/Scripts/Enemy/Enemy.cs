using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int health = 5;
    protected Transform player;
    protected Animator animator;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (player == null) return;
        MoveTowardsPlayer();
    }

    protected virtual void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        // Animator 파라미터가 존재할 경우만 isMoving 설정
        if (animator != null && animator.HasParameter("isMoving"))
        {
            bool isMoving = direction.magnitude > 0.1f;
            animator.SetBool("isMoving", isMoving);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (animator != null && animator.HasParameter("Hit"))
        {
            animator.SetTrigger("Hit");
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
