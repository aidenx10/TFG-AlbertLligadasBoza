using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Movements")]
    public float moveSpeed;

    [Header("Jump")]
    private bool canDoubleJump;
    public float jumpForce;
    public float bounceForce;

    [Header("Components")]
    public Rigidbody2D rb;

    [Header("Animator")]
    public Animator anim;
    private SpriteRenderer sr;

    [Header("Grounded")]
    public bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask groundLayer;

    [Header("Knock")]
    public float knockBackLength, knockBackForce;
    private float knockBackCounter;

    public bool stopInput;
    public int facingDirection = 1;
    public bool canShoot = false;

    [Header("Regulable Jump")]
    [Range(0, 1)] [SerializeField] private float multiplierJump;
    [SerializeField] private float multiplierGravity;
    public float gravityScale;
    public bool jumpButton = true;
    public bool jump;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        gravityScale = rb.gravityScale;
    }

    void Update()
    {
        if (!PauseMenu.instance.isPaused && !stopInput)
        {
            if (knockBackCounter <= 0)
            {
                rb.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y);

                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, groundLayer);

                if (isGrounded)
                {
                    canDoubleJump = true;
                }

                if (Input.GetButtonDown("Jump"))
                {
                    if (isGrounded)
                    {
                        AudioManager.instance.PlaySFX(1);
                        Jumping();
                    }
                    else if (canDoubleJump)
                    {
                        AudioManager.instance.PlaySFX(1);
                        Jumping();
                        canDoubleJump = false;
                    }
                }

                if (Input.GetButtonUp("Jump"))
                {
                    ButtonJumpUp();
                    jumpButton = true;
                }

                if (rb.velocity.x < 0)
                {
                    sr.flipX = true;
                    facingDirection = -1;
                }
                else if (rb.velocity.x > 0)
                {
                    sr.flipX = false;
                    facingDirection = 1;
                }
            }
            else
            {
                knockBackCounter -= Time.deltaTime;

                if (!sr.flipX)
                {
                    rb.velocity = new Vector2(-knockBackForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector3(knockBackForce, rb.velocity.y);
                }
            }
        }
        anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }

    public void Knockback()
    {
        knockBackCounter = knockBackLength;
        rb.velocity = new Vector2(0f, knockBackForce);
    }

    public void Bounce()
    {
        rb.velocity = new Vector2(rb.velocity.x, bounceForce);
    }

    private void Jumping()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
        jumpButton = false;
    }

    private void ButtonJumpUp()
    {
        if (rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * rb.velocity.y * (1 - multiplierJump), ForceMode2D.Impulse);
        }

        jumpButton = true;
        jump = true;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < 0 && !isGrounded)
        {
            rb.gravityScale = gravityScale * multiplierGravity;
            jumpButton = true;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }
}
