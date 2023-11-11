using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables dedicadas a componentes del jugador
    private Rigidbody2D rb;

    // Variables dedicadas al movimiento del jugador
    public float moveSpeed = 5.66f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("HorizontalMovement");
        float y = Input.GetAxisRaw("VerticalMovement");

        Vector2 walkDirection = new Vector2(x, y);
        Walk(walkDirection);
    }

    private void Walk(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }
}
