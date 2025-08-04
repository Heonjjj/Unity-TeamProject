using UnityEngine;

public class TankEnemy : Enemy
{
    [SerializeField] private float knockbackForce = 5f;

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DealDamageToPlayer(collision);

            // ³Ë¹é È¿°ú
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
