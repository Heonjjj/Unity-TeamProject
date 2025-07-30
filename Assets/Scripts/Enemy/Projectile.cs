using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("���Ÿ� ���� �ǰ�!");
            Destroy(gameObject);
        }
    }
}
