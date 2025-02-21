using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class Heal : Interactable
{
    public ParticleSystem particles;

    public GameObject tooltipObject;
    public Sprite tooltipSprite;
    public hp player;

    public int heal = 35;
    private GameObject toolTipInstance;

    private void Awake()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
    }

    public override void Interact()
    {
        if (toolTipInstance != null)
        {
            Destroy(toolTipInstance);
        }

        Destroy(gameObject);

        player.Heal(heal);
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

            toolTipInstance.GetComponent<SpriteRenderer>().sprite = tooltipSprite;
        }
        else
        {
            Destroy(toolTipInstance);
        }
    }
}
