using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    [SerializeField] private float speed;

    private Animator animator;
    private int currentIndex;
    private Vector2 currentPoint;
    private bool walking;
    private bool isDead;


    void Start()
    {
        animator = GetComponent<Animator>();
        currentPoint = points[0].position;

        ChooseDirection();

        walking = true;
    }


    void Update()
    {
        if (isDead) { return; }

        Walk();
    }

    private void Walk()
    {
        animator.SetBool("Walk", walking);

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
        animator.SetTrigger("Idle");
        ChooseNextPoint();

        yield return new WaitForSeconds(1);

        walking = true;
    }

    private void ChooseNextPoint()
    {
        currentIndex = ++currentIndex < points.Count ? currentIndex : 0;
        currentPoint = points[currentIndex].position;

        ChooseDirection();
    }

    private void ChooseDirection()
    {
        GetComponent<SpriteRenderer>().flipX = currentPoint.x < transform.position.x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead && collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Dead();
        }
    }

    public void Hit()
    {
        isDead = true;
        animator.SetBool("Walk", false);
        animator.SetTrigger("Dead");
        Destroy(GetComponent<Collider2D>(), 1);
        Destroy(GetComponent<Rigidbody2D>(), 1);
        Destroy(this, 2);
    }
}