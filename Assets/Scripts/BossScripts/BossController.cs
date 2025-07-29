using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private float patternDelay = 2f; // ���� ��� ����
    [SerializeField] private List<MonoBehaviour> patternList; // ���� ���
    
    private float patternTimer; // ���� ���ϱ��� ���� �ð�
    private Dictionary<IBossAttackPattern, float> cooldownTracker = new(); // �� ���Ϻ� ���� ��Ÿ��
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
                Debug.LogWarning($"{mono.name}�� IBossAttackPattern�� �������� �ʾҽ��ϴ�.");
            }
        }
    }

    private void Update()
    {
        UpdateCooldowns();

        patternTimer -= Time.deltaTime; // �� �����Ӹ��� timer ����
        if (patternTimer <= 0f) // 0�̵Ǹ� ���� ���� ���� �� ������ �ʱ�ȭ
        {
            if (TryActivateAvailablePattern())
            {
                patternTimer = patternDelay;
            }
        }

        TrackPlayer(); // ������ �÷��̾ ������
    }

    private void UpdateCooldowns() // ��� ������ ��ٿ� Ÿ�̸� ����
    {
        var keys = new List<IBossAttackPattern>(cooldownTracker.Keys);
        foreach (var key in keys)
        {
            cooldownTracker[key] -= Time.deltaTime;
        }
    }

    private bool TryActivateAvailablePattern() // ��Ÿ���� ���� ���� �� �ϳ� ������ ����
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
        
        Vector3 dir = (player.transform.position - transform.position).normalized; // ������ �÷��̾ ���� �̵�
        transform.position += dir * bossCharacter.moveSpeed * Time.deltaTime;
    }
}
