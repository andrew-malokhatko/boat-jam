using System;
using UnityEngine;

public class PlayerMovement : Sounds
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 8f;
    private float maxJumpPower = 12f;
    private float acceleration = 10f;
    private float deceleration = 20f;
    private float currentSpeed = 0f;
    private float sprintMultiplier = 1.5f;
    private float jumpTime = 0.2f;
    private float jumpTimer;
    private float normalDrag = 5f;
    private float iceDrag = 0f;

    private bool isFacingRight = true;
    private bool isGrounded;
    private bool isJumping;
    private bool isOnIce = false;

    private int maxJumps = 2;
    private int jumpCount;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    Vector2 boxSize = new Vector2(1.5f, 1.5f);
    bool isLocked = false;

    void Start()
    {
        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground");

        foreach (GameObject ground in groundObjects)
        {
            ground.layer = LayerMask.NameToLayer("Ground");
        }

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ice"))
        {
            isOnIce = true;
            rb.linearDamping = iceDrag;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ice"))
        {
            isOnIce = false;
            rb.linearDamping = normalDrag;
        }
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        bool attack = Input.GetButtonDown("Fire1");

        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.4f, groundLayer);

        if (isGrounded && !wasGrounded)
        {
            jumpCount = maxJumps;
        }

        animator.SetBool("isJumping", !isGrounded);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpCount > 0)
        {
            isJumping = true;
            jumpTimer = jumpTime;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            rb.gravityScale = 2.5f;
            jumpCount--;
			PlaySound(sounds[0]);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isJumping && jumpTimer > 0)
        {
            if (jumpTimer > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxJumpPower);
                jumpTimer -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJumping = false;
            rb.gravityScale = 3f;
        }

        if (attack)
        {
            animator.SetTrigger("isAttacking"); 
        }

        CheckInteraction();

        Flip();
    }

    private void FixedUpdate()
    {
        float targetSpeed = horizontal * speed;

        if (isOnIce)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 1f * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetSpeed *= sprintMultiplier;
        }

        if (horizontal != 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);

        if (rb.linearVelocity.y > 10f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 10f);
        }
        //if (rb.linearVelocity.y < -12f) {
        //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, -12f);
        //}

        animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0 || !isFacingRight && horizontal > 0)
        {
            isFacingRight = !isFacingRight;
            //transform.Rotate(180f, 0f, 0f);
            GetComponent<SpriteRenderer>().flipX = !isFacingRight;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        animator.SetBool("isJumping", false);
    }
    private void CheckInteraction()
    {
        if (!Input.GetKeyDown(KeyCode.E))
        {
            return;
        }

        // create a box of boxSize around the player and check for collisions
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0.0f, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            // if component is interactable 
            if (hit.transform.GetComponent<Interactable>())
            {
                hit.transform.GetComponent<Interactable>().Interact();
                return;
            }
        }
    }

    public void LockMovement()
    {
        isLocked = true;
    }

    public void UnlockMovement()
    {
        isLocked = false;
    }
}