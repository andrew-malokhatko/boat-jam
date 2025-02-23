using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timeRemaining;
    private float totalTime;

    private float width;
    private SpriteRenderer spriteRenderer;

    public void SetTime(float totalTime)
    {
        this.totalTime = totalTime;
        timeRemaining = totalTime;

        spriteRenderer = GetComponent<SpriteRenderer>();
        width = spriteRenderer.bounds.size.x;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining < 0)
        {
            Destroy(gameObject);
        }

        float scale = timeRemaining / totalTime;
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
    }
}
