using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private float patternDelay = 2f; // 패턴 사용 간격
    [SerializeField] private List<MonoBehaviour> patternList; // 패턴 목록
    
    private float patternTimer; // 다음 패턴까지 남은 시간
    private Dictionary<IBossAttackPattern, float> cooldownTracker = new(); // 각 패턴별 남은 쿨타임
    private BossCharacter bossCharacter;

    private void Start()
    {
        bossCharacter = GetComponent<BossCharacter>();
        patternTimer = patternDelay;

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

    private void Update()
    {
        UpdateCooldowns();

        patternTimer -= Time.deltaTime; // 매 프레임마다 timer 감소
        if (patternTimer <= 0f) // 0이되면 랜덤 공격 실행 후 딜레이 초기화
        {
            if (TryActivateAvailablePattern())
            {
                patternTimer = patternDelay;
            }
        }

        TrackPlayer(); // 보스는 플레이어를 추적함
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
        selected.Activate();
        cooldownTracker[selected] = selected.Cooldown;

        return true;
    }

    private void TrackPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;
        
        Vector3 dir = (player.transform.position - transform.position).normalized; // 보스가 플레이어를 향해 이동
        transform.position += dir * bossCharacter.moveSpeed * Time.deltaTime;
    }
}
