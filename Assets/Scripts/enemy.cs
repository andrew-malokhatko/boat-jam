using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    [SerializeField] private float speed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private int ForbiddenIndex;

    private Animator animator;
    private int currentIndex;
    private Vector2 currentPoint;
    private bool walking;
    private bool isDead;
    private bool isClimbing = false;
    private Rigidbody2D rb;
    private bool directionToRight = true;
    private bool isOnUp = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        currentPoint = points[0].position;
        rb = GetComponent<Rigidbody2D>();

        ChooseDirection();

        walking = true;
    }


    void Update()
    {
        if (isDead) {
            animator.SetTrigger("dead");
            StartCoroutine(FadeAndDestroy());
            walking = false;
            animator.SetBool("walk", false);
            //return; 
        }

        Walk();

        if (Physics2D.OverlapCircle(transform.position, 0.5f, LayerMask.GetMask("Ladder")) && Vector3.Distance(transform.position, currentPoint) < 0.3f)
        {
            isClimbing = true;
            ChooseNextPoint();
            StartClimbing();
        }

        if (isClimbing)
        {
            float vertical = Input.GetAxis("Vertical");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * climbSpeed);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    void StartClimbing()
    {
        walking = false;
        animator.SetBool("walk", false);
        //fix
        //animator.SetTrigger("climbing");

        // next code checks if next point under or over
        if (transform.position.y < currentPoint.y)
            isOnUp = true;
        else
            isOnUp = false;
            
        // climbing to up
        if (isOnUp)
        {
            //gameObject.
        }
    }

    IEnumerator FadeAndDestroy()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color color = sprite.color;

        yield return new WaitForSeconds(3f);

        float fadeTime = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime * 23 / fadeTime);

            Debug.Log("Alpha: " + alpha);

            color.a = alpha;
            sprite.color = color;
            yield return null;
        }

        Destroy(gameObject);
    }


    private void Walk()
    {
        animator.SetBool("run", walking);

        if (walking)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, currentPoint, step);

            if (Vector3.Distance(transform.position, currentPoint) < 0.3f)
            {
                StartCoroutine(Idle());
            }
        }
    }

    private IEnumerator Idle()
    {
        walking = false;
        animator.SetTrigger("idle");
        ChooseNextPoint();

        yield return new WaitForSeconds(1);

        walking = true;
    }

    // enemy movements
    private void ChooseNextPoint()
    {
        if (points.Count == currentIndex + 1)
        {
            directionToRight = false;
        }

        // if point is out of reach, so we cannot move next to the left point in array
        if (currentIndex - 1 == -1 || currentIndex - 1 == ForbiddenIndex)
        {
            directionToRight = true;
        }

        if (!directionToRight)
            currentIndex--;
        else
            currentIndex++;

        currentPoint = points[currentIndex].position;

        ChooseDirection();
    }

    // flips image of enemy (right of left)
    private void ChooseDirection()
    {
        GetComponent<SpriteRenderer>().flipX = currentPoint.x < transform.position.x;
    }

    // kill player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead && collision.transform.tag == "Player")
        {
            // FIX
            //collision.gameObject.GetComponent<Player>().Dead();
        }
    }

    public void Hit()
    {
        isDead = true;
        animator.SetBool("walk", false);
        animator.SetTrigger("dead");
        Destroy(GetComponent<Collider2D>(), 1);
        Destroy(GetComponent<Rigidbody2D>(), 1);
        Destroy(this, 2);
    }
}