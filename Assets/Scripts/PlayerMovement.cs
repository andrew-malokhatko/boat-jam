using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 boxSize = new Vector2(1.5f, 1.5f);

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 6f;
    private bool isFacingRight = true;
    private bool isGrounded = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        bool attack = Input.GetButtonDown("Fire1");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            animator.SetBool("isJumping", true);
            isGrounded = false;
        }
        else if (attack)
        {
            GetComponent<Animator>().Play("ATTACK", -1, 0f);
        }

        CheckInteraction();

        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0 || !isFacingRight && horizontal > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        animator.SetBool("isJumping", false);
    }

    private void CheckInteraction()
    {
        // Player interacts with Interactables on key "E"
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
}