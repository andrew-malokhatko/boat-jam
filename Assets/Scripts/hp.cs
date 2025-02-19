using UnityEngine;

public class hp : MonoBehaviour
{
    public int health = 100;

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
    }

}
