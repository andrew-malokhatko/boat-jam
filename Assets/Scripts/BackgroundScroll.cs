using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private float length;
    private float startPos;

    public float parallaxSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newX = transform.position.x + (parallaxSpeed * Time.deltaTime);

        // if moving left
        if (newX <= startPos - length)
        {
            newX = startPos;
        }

        // if moving right
        if (newX >= startPos + length)
        {
            newX = startPos;
        }

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
