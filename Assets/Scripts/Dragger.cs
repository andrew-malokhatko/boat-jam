using UnityEngine;

public class Dragger : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().setDead();
        }
        if (!collision.gameObject.CompareTag("Player"))
        {
            DestroyDagger();
        }
    }

    void DestroyDagger()
    {
        Destroy(gameObject);
    }
}
