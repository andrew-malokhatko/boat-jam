using UnityEngine;

public class hp : MonoBehaviour
{
    public int health = 50;
    private bool isDead = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Player HP: " + health);

        if (health <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Debug.Log("Player is dead!");
        isDead = true;

        animator.SetTrigger("isDead");
    }
}
