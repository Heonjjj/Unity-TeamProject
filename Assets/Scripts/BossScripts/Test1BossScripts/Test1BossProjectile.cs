using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1BossProjectile : MonoBehaviour
{
    private float damage;

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Character>(); // <���߿� PlayerCharacter�� ����
            if (player != null) // �÷��̾�� �߻�ü�� damage ��ġ��ŭ ������
            {
                player.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
