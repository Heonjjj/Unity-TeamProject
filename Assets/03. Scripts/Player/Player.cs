using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Character
{
    public static Player Instance;                              //�̱���

    //HP UI Test
    [SerializeField] private TMP_Text hpText;

    //damageCooldown Test
    [SerializeField] private float damageCooldown = 1f;
    private float lastDamageTime = -Mathf.Infinity;

    [SerializeField] private Sprite idleSprite;                 //���� ���� �̹���

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer mainRenderer;

    private bool isAttacking = false;
    private float attackAnimTime = 0.2f;
    private PlayerMove move;

    //�� ��ȯ �� ����
    void Awake()
    {
        //�� ��ȯ �� �ߺ� ���� ���� 
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
        if (attackRange == 0) attackRange = 14f;                           //���� ���� ���� ����

        move = GetComponent<PlayerMove>();

        base.Start();                               //�ִ� ü������ �����ֱ�         

        currentHP = 5f;
        
        UpdateHPText();                             //HP UI Test
    }

    void Update()
    {
        Vector2 movement = move != null ? move.Movement : Vector2.zero;

        //���� �� �ִϸ����� ���� ���� ���� ǥ��
        if (isAttacking)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        //�̵� ���⿡ ���� �̹��� ����
        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            mainRenderer.flipX = movement.x > 0;

        else
            mainRenderer.flipX = false;
        
        animator.SetBool("isWalking", movement != Vector2.zero);
    }

    //���� �������� �������� �����ֱ�
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
