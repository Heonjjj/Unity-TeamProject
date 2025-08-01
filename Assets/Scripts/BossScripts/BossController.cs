using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BossController : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> patternList; // 패턴 목록
    [SerializeField] private Transform bossSprite;
    [SerializeField] private float attackInterval = 1f;

    private Dictionary<IBossAttackPattern, float> cooldownTracker = new(); // 각 패턴별 남은 쿨타임
    private BossCharacter bossCharacter;
    private float attackTimer;

    private Animator anim;
    private Vector3 lastPosition;

    private Rigidbody2D rb;

    private void Start()
    {
        bossCharacter = GetComponent<BossCharacter>();
        rb = GetComponent<Rigidbody2D>();

        anim = bossSprite.GetComponent<Animator>();
        lastPosition = transform.position;

        foreach (var mono in patternList)
        {
            if (mono is IBossAttackPattern pattern)
            {
                cooldownTracker[pattern] = 0f;
            }
            else
            {
                Debug.LogWarning($"{mono.name}은 IBossAttackPattern을 구현하지 않았습니다.");
            }
        }
    }

    private void FixedUpdate()
    {
        UpdateCooldowns();
        UpdateSpeedAnim();

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            TryActivateAvailablePattern();
            attackTimer = attackInterval;
        }

        TrackPlayer();
    }

    private void UpdateCooldowns() // 모든 패턴의 쿨다운 타이머 감소
    {
        var keys = new List<IBossAttackPattern>(cooldownTracker.Keys);
        foreach (var key in keys)
        {
            cooldownTracker[key] -= Time.deltaTime;
        }
    }

    private bool TryActivateAvailablePattern() // 쿨타임이 끝난 패턴 중 하나 무작위 실행
    {
        List<IBossAttackPattern> availablePatterns = new();

        foreach (var pattern in cooldownTracker.Keys)
        {
            if (cooldownTracker[pattern] <= 0f)
                availablePatterns.Add(pattern);
        }

        if (availablePatterns.Count == 0)
            return false;

        int index = Random.Range(0, availablePatterns.Count);
        IBossAttackPattern selected = availablePatterns[index];

        if (anim != null)
            anim.SetTrigger("IsAttack");

        selected.Activate();

        cooldownTracker[selected] = selected.Cooldown;

        return true;
    }

    private void TrackPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        Vector3 dir = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance > 0.1f)
        {
            Vector3 moveDelta = dir * bossCharacter.moveSpeed;
            rb.MovePosition(transform.position + moveDelta * Time.fixedDeltaTime);
        }

        if (bossSprite != null)
        {
            Vector3 scale = bossSprite.localScale;
            scale.x = (player.transform.position.x < transform.position.x) ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            bossSprite.localScale = scale;
        }
    }

    private void UpdateSpeedAnim()
    {
        if (anim == null) return;

        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        anim.SetFloat("Speed", speed);

        lastPosition = transform.position;
    }
}
