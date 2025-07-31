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
        HandleDirection(); // �ڵ� ���� ��ȯ
    }

    protected virtual void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        // Animator �Ķ���Ͱ� ������ ��츸 isMoving ����
        if (animator != null && animator.HasParameter("isMoving"))
        {
            bool isMoving = direction.magnitude > 0.1f;
            animator.SetBool("isMoving", isMoving);
        }
    }

    private void HandleDirection()
    {
        if (player == null) return;

        Vector2 direction = player.position - transform.position;

        // �¿� ���� ó��
        if (Mathf.Abs(direction.x) > 0.01f) // ���� 0�� ���� ����
        {
            Vector3 localScale = transform.localScale;
            localScale.x = direction.x > 0 ? -Mathf.Abs(localScale.x) : Mathf.Abs(localScale.x);
            transform.localScale = localScale;
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
