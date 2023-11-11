using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    // Variables dedicadas a componentes del jugador
    private Rigidbody2D rb;

    // Variables dedicadas a la detección de colision en el suelo
    public LayerMask groundLayer;
    public bool grounded;
    float groundLength = 0.6f;
    public Vector3 colliderOffset;

    // Variables dedicadas al dash
    private float dashingVelocity = 20f;
    private float dashingTime = 0.1f;
    private Vector2 dashingDirection;
    private bool isDashing;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var x = Input.GetAxisRaw("HorizontalMovement");
        var y = Input.GetAxisRaw("VerticalMovement");
        bool dashInput = Input.GetButtonDown("Dash");

        grounded = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) ||
            Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

        if (dashInput && canDash)
        {
            isDashing = true;
            canDash = false;
            dashingDirection = new Vector2(Input.GetAxisRaw("HorizontalMovement"), Input.GetAxisRaw("VerticalMovement"));

            if (dashingDirection == Vector2.zero)
            {
                dashingDirection = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(StopDash());
        }

        if (isDashing)
        {
            rb.velocity = dashingDirection.normalized * dashingVelocity;
            return;
        }

        if (grounded)
        {
            canDash = true;
        }
    }

    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
    }

    
}
