using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2BossPattern_Dash : MonoBehaviour, IBossAttackPattern
{
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float cooldown = 10f;

    private Transform bossTransform;

    public float Damage => 0f;
    public float Cooldown => cooldown;

    private bool isDashing;

    public void Activate()
    {
        if (!isDashing)
        {
            StartCoroutine(DashCoroutine());
            Debug.Log("보스 돌진 시작");
        }
    }

    private IEnumerator DashCoroutine()
    {
        isDashing = true;

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) yield break;

        Vector3 start = transform.position;
        Vector3 dir = (player.transform.position - start).normalized;
        Vector3 end = start + dir * dashDistance;

        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            float t = elapsed / dashDuration;
            transform.position = Vector3.Lerp(start, end, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
        isDashing = false;
    }
}
