using UnityEngine;
using UnityEngine.UI;

public class hp : MonoBehaviour
{
    public int maxHP;
    private int health;
    private bool isDead = false;
    private Animator animator;
    public Image healthBar;

    private void Start()
    {
        animator = GetComponent<Animator>();
        health = maxHP;
    }

    public void Update()
    {
        healthBar.fillAmount = (float)health / maxHP;
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
