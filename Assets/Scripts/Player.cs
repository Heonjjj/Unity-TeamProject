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
        moveSpeed = 6f;                             //���� ���� ���� ����

        base.Start();                               //�ִ� ü������ �����ֱ�
        rb = GetComponent<Rigidbody2D>();           
    }

    void Update()
    {   //�̵� ���� �ڵ�
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        rb.velocity = movement * moveSpeed;
    }
}
