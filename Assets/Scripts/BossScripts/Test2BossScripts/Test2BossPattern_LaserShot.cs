using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2BossPattern_LaserShot : BossProjectilePatternBase, IBossAttackPattern
{
    [SerializeField] private float fireRate = 0.1f;          // 발사 간격
    [SerializeField] private float totalDuration = 1f;     // 전체 연사 시간
    [SerializeField] private float projectileSpeed = 8f;    // 화살 속도
    [SerializeField] private float cooldown = 10f;            // 쿨타임

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
