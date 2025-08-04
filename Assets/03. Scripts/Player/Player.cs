using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Character
{
    public static Player Instance;                              //싱글톤

    //HP UI Test
    [SerializeField] private TMP_Text hpText;

    //damageCooldown Test
    [SerializeField] private float damageCooldown = 1f;
    private float lastDamageTime = -Mathf.Infinity;

    [SerializeField] private Sprite idleSprite;                 //정지 상태 이미지

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer mainRenderer;

    private bool isAttacking = false;
    private float attackAnimTime = 0.2f;
    private PlayerMove move;

    //씬 전환 후 유지
    void Awake()
    {
        //씬 전환 시 중복 생성 방지 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    protected override void Start()
    {
        if (maxHP == 0) maxHP = 8f;
        if (currentHP == 0) currentHP = 5f;
        if (attackPower == 0) attackPower = 1f;
        if (attackSpeed == 0) attackSpeed = 1f;
        if (moveSpeed == 0) moveSpeed = 3f;
        if (attackRange == 0) attackRange = 14f;                           //스탯 설정 수정 가능

        move = GetComponent<PlayerMove>();

        base.Start();                               //최대 체력으로 맞춰주기         

        currentHP = 5f;
        
        UpdateHPText();                             //HP UI Test
    }

    void Update()
    {
        Vector2 movement = move != null ? move.Movement : Vector2.zero;

        //공격 중 애니메이터 끄고 정지 상태 표시
        if (isAttacking)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        //이동 방향에 따라 이미지 변경
        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            mainRenderer.flipX = movement.x > 0;

        else
            mainRenderer.flipX = false;
        
        animator.SetBool("isWalking", movement != Vector2.zero);
    }

    //공격 방향으로 정지상태 보여주기
    public void PlayAttackDirection(Vector2 attackDir)
    {
        if (isAttacking) return;
        isAttacking = true;

        bool isLeft = attackDir.x > 0;
        animator.SetBool("isWalking", false);

        StartCoroutine(EndAttackAnim());
    }

    private IEnumerator EndAttackAnim()
    {
        yield return new WaitForSeconds(attackAnimTime);
        isAttacking = false;
    }

    //HP UI Test
    public override void TakeDamage(float damage)
    {
        //damageCooldown Test
        if (Time.time - lastDamageTime < damageCooldown)
            return;

        //damageCooldown Test
        lastDamageTime = Time.time;

        base.TakeDamage(damage);
        UpdateHPText();
    }

    //HP UI Test
    private void UpdateHPText()
    {
        if (hpText != null)
            hpText.text = $":HP: {currentHP} / {maxHP}";
    }
    public void RefreshStatsUI()
    {
        UpdateHPText();
    }
}
