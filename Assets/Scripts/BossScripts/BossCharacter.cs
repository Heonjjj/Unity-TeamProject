using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharacter : Character
{
    [Header("보스 데이터 (ScriptableObject")]
    [SerializeField] private BossStats bossStats;
    [SerializeField] private float contactDamage = 1f;

    public event System.Action<float> OnHPChanged;
    public event System.Action OnBossDie;

    public string BossName => bossStats.BossName;
    public float MaxHP => bossStats.MaxHP;
    public float MoveSpeed => bossStats.MoveSpeed;

    private Animator animator;
    private Coroutine hitRoutine;

    protected void Awake()
    {
        maxHP = bossStats.MaxHP;
        moveSpeed = bossStats.MoveSpeed;
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        OnHPChanged?.Invoke(currentHP);

        if (animator != null)
        {
            if (hitRoutine != null) StopCoroutine(hitRoutine);
            hitRoutine = StartCoroutine(PlayHitAnimation());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.collider.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(contactDamage);
                Debug.Log($"보스 충돌: 플레이어에게 {contactDamage} 데미지");
            }
        }
    }

    private IEnumerator PlayHitAnimation()
    {
        animator.SetBool("IsHit", true);
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("IsHit", false);
    }

    protected override void Die()
    {
        OnBossDie?.Invoke();
        Destroy(gameObject, 1f);
        base.Die();
    }

    public void SetHP(float newHP)  //테스트용
    {
        currentHP = Mathf.Clamp(newHP, 0, MaxHP);
        OnHPChanged?.Invoke(currentHP);

        if (currentHP <= 0)
        {
            Die(); // 강제로 죽이기
        }
    }
}
