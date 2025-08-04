using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;
    public float lifetime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifetime); // 일정 시간 지나면 제거
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 플레이어 태그 확인
        {
            Character playerCharacter = collision.GetComponent<Character>();
            if (playerCharacter != null)
            {
                playerCharacter.TakeDamage(damage); // 플레이어 HP 감소
                Debug.Log("Player hit by enemy projectile! HP: " + playerCharacter.currentHP);
            }
            Destroy(gameObject); // 발사체 제거
        }
    }
}
