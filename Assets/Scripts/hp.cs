using UnityEngine;
using UnityEngine.UI;

public class hp : MonoBehaviour
{
    public int maxHP;
    private int health;
    private bool isDead = false;
    private Animator animator;
    private Image healthBar;

    private void Start()
    {
        healthBar = transform.Find("Canvas/Background bar/Health bar").GetComponent<Image>();
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

    public void Heal(int heal)
    {
        health += heal;

        if (health > maxHP)
        {
            health = maxHP;
        }
    }

    public void Dead()
    {
        Debug.Log("Player is dead!");
        isDead = true;

        animator.SetTrigger("isDead");
    }
}
