using UnityEngine;

public class DashEnemy : Enemy
{
    [SerializeField] private float dashRange = 5f;
    [SerializeField] private float dashSpeed = 10f;
    private bool isDashing = false;

    protected override void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (!isDashing && distance <= dashRange)
        {
            StartCoroutine(Dash());
        }
        else if (!isDashing)
        {
            base.Update();
        }
    }

    private System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * dashSpeed;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        isDashing = false;
    }
}
