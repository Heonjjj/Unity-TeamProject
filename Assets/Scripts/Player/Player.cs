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
        attackRange = 10f;                           //스탯 설정 수정 가능

        base.Start();                               //최대 체력으로 맞춰주기         
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
