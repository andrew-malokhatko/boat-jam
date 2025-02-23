using UnityEngine;

public class Hole : Interactable
{
    public ParticleSystem   waterParticles;

    public GameObject       tooltipObject;
    public Sprite           toolipSprite;

    public Sprite[]         holes;

    // used to remove hole from the scene
    private ParticleSystem waterInstance;
    private GameObject toolTipInstance; 

    private void Awake()
    {
        waterInstance = Instantiate(waterParticles, transform.position, Quaternion.identity);

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = holes[Random.Range(0, holes.Length)];
    }

    public void Destroy()
    {
        if (waterInstance != null)
        {
            Destroy(waterInstance.gameObject);
        }

        if (toolTipInstance != null)
        {
            Destroy(toolTipInstance);
        }
        Destroy(gameObject);
    }

    public override void Interact()
    {
        Destroy();
        Debug.Log("Interacted with HOLE");
    }

    public override void ShowTooltip(bool show)
    {
        if (show)
        {
            if (toolTipInstance != null)
            {
                Destroy(toolTipInstance);
            }

            Vector3 pos = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z);
            toolTipInstance = Instantiate(tooltipObject, pos, Quaternion.identity);

            toolTipInstance.GetComponent<SpriteRenderer>().sprite = toolipSprite;
        }
        else
        {
            Destroy(toolTipInstance); 
        }
    }
}
