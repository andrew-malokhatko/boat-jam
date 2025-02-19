using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Node currentNode;
    [SerializeField] private float speed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float attackRate = 1.0f;
    public Tilemap tilemap;

    private Animator animator;
    private Vector2 currentPoint;
    private bool walking;
    private bool isDead;
    private bool isClimbing = false;
    private Rigidbody2D rb;
    private bool isOnLadder = false;
    private bool isNextPointOnLadder = false;
    private float attackCooldown = 0f;


    void Start()
    {
        animator = GetComponent<Animator>();
        currentPoint = currentNode.transform.position;
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
            animator.SetBool("run", false);

            //return; 
        }

        if (IsPointOnLadder(currentPoint))
        {
            isNextPointOnLadder = true;
        }
        else
            isNextPointOnLadder = false;

        // starts climbing at ladder
        if (isNextPointOnLadder && isOnLadder && Vector3.Distance(transform.position, currentPoint) < 0.15f)
        {
            isClimbing = true;
            ChooseNextPoint();
            StartClimbing();
        }

        if (isClimbing)
        {
            rb.gravityScale = 0;
            float step = climbSpeed * Time.deltaTime;
            float newY = Mathf.MoveTowards(transform.position.y, currentPoint.y, step);
            transform.position = new Vector2(transform.position.x, newY);
        }
        else
        {
            rb.gravityScale = 1;
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        else
            Walk();
    }

    public bool IsPointOnLadder(Vector3 worldPosition)
    {
        Vector3Int tilePosition = tilemap.WorldToCell(worldPosition);
        return tilemap.HasTile(tilePosition);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            isOnLadder = true;
        }
    }

    void OnTriggerLeft2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            isOnLadder = false;
        }
    }

    void StartClimbing()
    {
        animator.SetBool("run", false);
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

            color.a = alpha;
            sprite.color = color;
            yield return null;
        }

        Destroy(gameObject);
    }


    private void Walk()
    {
        //bool checkAnim = false;
        //if (!attacking)
        //{
        //    animator.SetBool("run", walking);
        //}
        //else
        //{
        //    animator.SetBool("run", false);
        //}
        //Debug.Log(walking);
        animator.SetBool("run", walking);

        if (walking)
        {
            float step = climbSpeed * Time.deltaTime;
            float newX = Mathf.MoveTowards(transform.position.x, currentPoint.x, step);
            transform.position = new Vector2(newX, transform.position.y);

            // fix
            if (Vector3.Distance(transform.position, currentPoint) < 0.2f && !isClimbing && !isNextPointOnLadder)
            {
                //StartCoroutine(Idle());
                ChooseNextPoint();
            }
            if (Vector3.Distance(transform.position, currentPoint) < 0.2f && isClimbing)
            {
                ChooseNextPoint();
                isNextPointOnLadder = false;
                isOnLadder = false;
                isClimbing = false;
                rb.gravityScale = 1;
            }
        }
    }

    private IEnumerator Idle()
    {
        walking = false;
        animator.SetTrigger("idle");
        

        yield return new WaitForSeconds(0.01f);

        walking = true;
    }

    private void ChooseNextPoint()
    {
        currentNode = currentNode.nextPoint;
        currentPoint = currentNode.transform.position;

        ChooseDirection();
    }

    private void ChooseDirection()
    {
        GetComponent<SpriteRenderer>().flipX = currentPoint.x < transform.position.x;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isDead && collision.transform.tag == "Player" && attackCooldown <= 0f)
        {
            hp playerScript = collision.gameObject.GetComponent<hp>();


            if (playerScript != null)
            {
                animator.SetTrigger("attack");
                playerScript.TakeDamage(25);
                attackCooldown = attackRate;
            }
        }
    }

    public void Hit()
    {
        isDead = true;
        animator.SetBool("run", false);
        animator.SetTrigger("dead");
        Destroy(GetComponent<Collider2D>(), 1);
        Destroy(GetComponent<Rigidbody2D>(), 1);
        Destroy(this, 2);
    }
}