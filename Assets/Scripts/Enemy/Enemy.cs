using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int health = 5;
    protected Transform player;
    protected Animator animator;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponentInChildren<Animator>(); 
    }

    protected virtual void Update()
    {
        if (player == null) return;

        MoveTowardsPlayer();
    }

    protected virtual void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        // 이동 여부 체크
        bool isMoving = direction.magnitude > 0.1f;
        animator.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetTrigger("Hit"); //트리거로 피격 애니메이션 재생 후 Idle 모션으로 돌아감

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
