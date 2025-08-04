using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2BossPattern_LaserShot : BossProjectilePatternBase, IBossAttackPattern
{
    [SerializeField] private float fireRate = 0.1f;          // �߻� ����
    [SerializeField] private float totalDuration = 1f;     // ��ü ���� �ð�
    [SerializeField] private float projectileSpeed = 8f;    // ȭ�� �ӵ�
    [SerializeField] private float cooldown = 10f;            // ��Ÿ��

    public float Damage => 1f;
    public float Cooldown => cooldown;

    public void Activate()
    {
        StartCoroutine(FireRepeatedly());
    }

    private IEnumerator FireRepeatedly()
    {
        float elapsed = 0f;

        while (elapsed < totalDuration)
        {
            ShootProjectiles(1, projectileSpeed, 0f, false, Damage);
            yield return new WaitForSeconds(fireRate);
            elapsed += fireRate;
        }
    }
}
