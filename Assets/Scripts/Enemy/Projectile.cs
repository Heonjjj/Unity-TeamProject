using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;
    private bool isDestroyed = false; // �ߺ� ���� ���� ����

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
        if (isDestroyed == true) return; // �̹� ���ŵ� ��� �ٽ� �������� ����
        isDestroyed = true;
        Destroy(gameObject);
    }
}
