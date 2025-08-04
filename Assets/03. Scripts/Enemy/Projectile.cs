using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;
    public float lifetime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifetime); // ���� �ð� ������ ����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // �÷��̾� �±� Ȯ��
        {
            Character playerCharacter = collision.GetComponent<Character>();
            if (playerCharacter != null)
            {
                playerCharacter.TakeDamage(damage); // �÷��̾� HP ����
                Debug.Log("Player hit by enemy projectile! HP: " + playerCharacter.currentHP);
            }
            Destroy(gameObject); // �߻�ü ����
        }
    }
}
