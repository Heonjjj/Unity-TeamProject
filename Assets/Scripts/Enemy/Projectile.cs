using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;
    private bool isDestroyed = false; // 중복 제거 오류 방지

    private void OnEnable()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private System.Collections.IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroyProjectile();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Wall"))
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        if (isDestroyed == true) return; // 이미 제거된 경우 다시 실행하지 않음
        isDestroyed = true;
        Destroy(gameObject);
    }
}
