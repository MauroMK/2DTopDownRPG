using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    
    public float moveSpeed;

    private Rigidbody2D rb;
    
    Vector2 movement;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Swap sprite direction, wether you're going right or left
        if(movement.x > 0)
        {
            transform.localScale = Vector2.one;
        }
        if(movement.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        // Moves the character
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }
}
