using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Character
{
    //HP UI Test
    [SerializeField] private TMP_Text hpText;

    //damageCooldown Test
    [SerializeField] private float damageCooldown = 1f;
    private float lastDamageTime = -Mathf.Infinity;

    protected override void Start()
    {
        maxHP = 5f;
        attackPower = 1f;
        attackSpeed = 1f;
        moveSpeed = 6f;
        attackRange = 10f;                           //���� ���� ���� ����

        base.Start();                               //�ִ� ü������ �����ֱ�         
        UpdateHPText();                             //HP UI Test
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
}
