using UnityEngine;

public class AddBoxCollider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(2.0f, 1.0f);
        boxCollider.offset = new Vector2(0f, -0.5f);
    }
}
