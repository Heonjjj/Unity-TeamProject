using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Character character;
    private Rigidbody2D rb;

    public Vector2 Movement { get; private set; }

    void Awake()
    {
        character = GetComponent<Character>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Movement = new Vector2(horizontal, vertical).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = Movement * character.moveSpeed;
    }
}
