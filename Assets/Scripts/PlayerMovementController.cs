using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    //////////////////////////
    // Seccion de variables //
    //////////////////////////
    // Variables dedicadas a componentes del jugador
    private Rigidbody2D rb;

    // Variables dedicadas al movimiento del jugador
    float x;
    float y;
    public float moveSpeed = 5.66f;
    private bool isFacingRight = true;

    // Variables dedicadas a parametros de velocidad de salto y gravedad
    private float jumpSpeed = 10f;
    private float gravity = 1f;
    private float fallMultiplier = 8f;
    private float linearDrag = 20f;

    // Variables dedicadas a la detección de colision en el suelo
    public LayerMask groundLayer;
    public bool grounded;
    float groundLength = 0.6f;
    private Vector3 colliderOffset = new Vector3(0.29f, 0.18f, 0f);

    // Variables dedicadas al dash
    private float dashingVelocity = 25f;
    private float dashingTime = 0.1f;
    private Vector2 dashingDirection;
    private bool isDashing;
    private bool canDash = true;

    // Variables dedicadas al buffer de salto
    private float jumpDelay = 0.25f;
    private float jumpTimer;

    // Variables dedicadas al wall-jump y wall-slide
    private bool isWallSliding;
    private float wallSlidingSpeed = 1f;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform wallCheck;
    [SerializeField] LayerMask wallLayer;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    public float wallJumpingDuration = 0.2f;
    private Vector2 wallJumpingPower = new Vector2(10f, 8f);

    //Variable animator
    private Animator animator;


    ///////////////////////
    // Seccion de codigo //
    ///////////////////////
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //componente
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        FixedUpdate();
        x = Input.GetAxisRaw("HorizontalMovement");
        y = Input.GetAxisRaw("VerticalMovement");
        bool dashInput = Input.GetButtonDown("Dash");

        if (!isWallJumping)
        {
            if(isFacingRight && x < 0f || !isFacingRight && x > 0f)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }

        flipWallCheck(isFacingRight);

        // Salto
        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
            //animacion
            animator.SetBool("Jumping", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
        }

        // Para saber si el jugador esta en el suelo
        grounded = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) ||
            Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);
        
        // Caida rapida
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravity * fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = gravity * (fallMultiplier / 2);
        }
        

        // Condicionales y metodos referentes al dash
        if (dashInput && canDash)
        {
            isDashing = true;
            canDash = false;
            dashingDirection = new Vector2(Input.GetAxisRaw("HorizontalMovement"), Input.GetAxisRaw("VerticalMovement"));
            //animacion dash
            animator.SetBool("Dash", true);
            if (dashingDirection == Vector2.zero)
            {
                dashingDirection = new Vector2(transform.localScale.x, 0);
               
            }
            StartCoroutine(StopDash());
        }

        if (isDashing)
        {
            rb.velocity = dashingDirection.normalized * CalculateDashAmount();
            return;
        }

        if (grounded)
        {
            canDash = true;
        }
        // activar/desactivar animacion de caida
        if(rb.velocity.y<0)
        {
            animator.SetBool("Fall", true);
        }
        else
        {
            animator.SetBool("Fall", false);
        }
        // Condicionales y metodos referentes al wall-slide y wall-jump
        WallSlide();
        WallJump();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + colliderOffset, Vector2.down);
        Gizmos.DrawRay(transform.position - colliderOffset, Vector2.down);
    }

    // Metodos referentes a caminar
    private void Walk(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        //Activar animaicon de caminar
        if(x!=0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    // Metodos referentes al salto
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0f;
    }

    void FixedUpdate()
    {
        if(!isWallJumping && !isDashing)
            Walk(new Vector2(x, y));

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


    // Metodos referentes al dash
    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        //detener animacion dash
        animator.SetBool("Dash", false);
    }

    private float CalculateDashAmount()
    {
        // Si el dash se hace en vertical
        if(dashingDirection.normalized.x == 0)
        {
            dashingVelocity = 16f;
            dashingTime = 0.08f;
        }
        // Si el dash se hace en alguna de las diagonales
        else if (dashingDirection.normalized.x < 1 && dashingDirection.normalized.x > 0 || dashingDirection.normalized.x > -1 && dashingDirection.normalized.x < 0)
        {
            dashingVelocity = 17.4f;
            dashingTime = 0.08f;
        }
        else
        {
            dashingVelocity = 24f;
            dashingTime = 0.12f;
        }

        return dashingVelocity;
    }


    // Metodos referentes al wall-jump y wall-slide
    private void flipWallCheck(bool isLookingRight)
    {
        if (!isLookingRight)
            wallCheck.position = new UnityEngine.Vector2(playerTransform.position.x - 0.5f, playerTransform.position.y);
        else
            wallCheck.position = new UnityEngine.Vector2(playerTransform.position.x + 0.5f, playerTransform.position.y);
    }
    
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !grounded && x != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            //On wall del animator
            animator.SetBool("Onwall", true);
        }
        else
        {
            isWallSliding = false;
            //Onwall animator
            animator.SetBool("Onwall", false);
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
            wallJumpingCounter -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if(transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }
}
