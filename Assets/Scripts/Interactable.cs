using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(Collider2D))] // Ensures a Collider is attached
public class Interactable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowTooltip(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowTooltip(false);
        }
    }

    public virtual void ShowTooltip(bool show)
    {
        Debug.Log("ToolTip for some Interactable object is not binded");
    }

    public virtual void Interact()
    {
        Debug.LogError("Interact method was called on Interactable which is abstract class");
    }
}
