using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Rigidbody2D rb;

    protected override void Start()
    {
        maxHP = 100f;
        attackPower = 10f;
        attackSpeed = 1f;
        moveSpeed = 6f;                             //스탯 설정 수정 가능

        base.Start();                               //최대 체력으로 맞춰주기
        rb = GetComponent<Rigidbody2D>();           
    }

    void Update()
    {   //이동 관련 코드
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        rb.velocity = movement * moveSpeed;
    }
}
