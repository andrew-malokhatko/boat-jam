using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DraggerPlayer : MonoBehaviour
{
    public GameObject daggerPrefab;
    public float throwForce = 10f;
    public float rotationSpeed = 250f;
    public float coolDownAttack = 0.75f;
    private float coolDown = 0f;
    private bool canAttack = true;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            ThrowDagger();
            canAttack = false;
        }

        if (!canAttack)
        {
            if (coolDown > coolDownAttack)
            {
                coolDown = 0;
                canAttack = true;
            }
            else
            {
                coolDown += Time.deltaTime;
            }
        }
    }

    void ThrowDagger()
    {
        Vector3 mousePosition = Input.mousePosition;
        float distanceToSpawn = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
        mousePosition.z = distanceToSpawn;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPosition.z = 0f;

        Vector2 throwDirection = ((Vector2)(targetPosition - transform.position)).normalized;

        GameObject dagger = Instantiate(daggerPrefab, transform.position + new Vector3(0, 0.3f), Quaternion.identity);

        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D daggerCollider = dagger.GetComponent<Collider2D>();
        if (playerCollider != null && daggerCollider != null)
        {
            Physics2D.IgnoreCollision(daggerCollider, playerCollider);
        }

        Rigidbody2D rb = dagger.GetComponent<Rigidbody2D>();

        rb.linearVelocity = throwDirection * throwForce;
        rb.angularVelocity = rotationSpeed;
    }
}
