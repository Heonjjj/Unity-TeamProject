using UnityEngine;

public class MeleeEnemy : Enemy
{
    protected override void Update()
    {
        base.Update();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            DealDamageToPlayer();
        }
    }

}
