using System.Collections;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Wheel : Interactable
{
    // Tooltip & Particles
    public ParticleSystem steeringParticles;
    public GameObject tooltipObject;
    public Sprite toolipSprite;
    private GameObject toolTipInstance;

    // Steering & pulse
    public float pulseSpeed = 1.1f;

    public bool requiresSteering { get; set; }
    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    // keys that are required to press to perfrom steering action
    private string[] instructionKeys = { "w", "a", "s", "d" };
    private GameObject instructionInstance;
    public Sprite[] instructionSprites;

    bool isInteracting = false;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void Update()
    {
        Pulse();
    }

    public void Pulse()
    {
        if (requiresSteering || isInteracting)
        {
            float t = Mathf.PingPong(Time.time * pulseSpeed, 1f);
            spriteRenderer.color = Color.Lerp(originalColor, Color.red, t);
        }
    }

    public override void Interact()
    {
        if (isInteracting || !requiresSteering)
        {
            return;
        }

        // destroy tooltip instance, it obscures instructions
        Destroy(toolTipInstance);

        StartCoroutine(StartInteractionCycle());

        requiresSteering = false;
        isInteracting = true;
    }

    private IEnumerator StartInteractionCycle()
    {
        ShuffleInstructions();

        int i = 0;
        spawnActionKeyTooltip(i);

        while (i < instructionKeys.Length)
        {
            if (Input.GetKeyDown(instructionKeys[i]))
            {
                // destroy last instruction instance before assigning new one
                Destroy(instructionInstance);
                instructionInstance = null;

                i++;

                if (i < instructionKeys.Length)
                {
                    spawnActionKeyTooltip(i);
                }

                // particles are removed automatically
                Instantiate(steeringParticles, transform.position, Quaternion.identity);
            }

            yield return null;
        }

        // reset the color after the end of interaction (due to the pulse)
        spriteRenderer.color = Color.white;

        Destroy(instructionInstance);
        isInteracting = false;
    }

    private void ShuffleInstructions()
    {
        var rnd = new System.Random();
        var shuffled = instructionKeys.Zip(instructionSprites, (key, sprite) => (key, sprite))
                                      .OrderBy(_ => rnd.Next())
                                      .ToArray();

        instructionKeys = shuffled.Select(x => x.key).ToArray();
        instructionSprites = shuffled.Select(x => x.sprite).ToArray();
    }

    public void spawnActionKeyTooltip(int index)
    {
        Vector3 pos = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z);
        instructionInstance = Instantiate(tooltipObject, pos, Quaternion.identity);
        instructionInstance.GetComponent<SpriteRenderer>().sprite = instructionSprites[index];
    }

    public override void ShowTooltip(bool show)
    {
        if (show && requiresSteering && !isInteracting)
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