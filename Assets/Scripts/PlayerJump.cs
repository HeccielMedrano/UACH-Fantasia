using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    // Variables dedicadas a componentes del jugador
    private Rigidbody2D rb;

    // Variables dedicadas a parametros de velocidad de salto y gravedad
    private float jumpSpeed = 10f;
    private float gravity = 1f;
    private float fallMultiplier = 6f;
    private float linearDrag = 10f;

    // Variables dedicadas a la detección de colision en el suelo
    public LayerMask groundLayer;
    public bool grounded;
    float groundLength = 0.6f;
    public Vector3 colliderOffset;

    // Variables dedicadas al buffer de salto
    private float jumpDelay = 0.25f;
    private float jumpTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FixedUpdate();
        grounded = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) ||
            Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0f;
    }

    void FixedUpdate()
    {
        if (jumpTimer > Time.time && grounded)
            Jump();
        
        ModifyPhysics();
    }

    void ModifyPhysics()
    {
        if (grounded)
            rb.gravityScale = 0f;
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;

            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }



    /*void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButton("Jump"))
            rb.velocity = Vector2.up * jumpForce;

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump"))
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (lowFallMultiplier - 1) * Time.deltaTime;
        }
    }*/
}
