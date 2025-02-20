using UnityEngine;

public class Wheel : Interactable
{
    public ParticleSystem steeringParticles;
    public GameObject tooltipObject;
    public Sprite toolipSprite;

    private GameObject toolTipInstance;
    private ParticleSystem particlesInstance;

    public override void Interact()
    {
        particlesInstance = Instantiate(steeringParticles, transform.position, Quaternion.identity);
        //particlesInstance.transform.Rotate(-90.0f, 0.0f, 0.0f);
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
