using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float originalZoom;
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        originalZoom = cam.orthographicSize; // Store initial zoom level
    }

    public void ZoomIn(float zoomFactor)
    {
        cam.orthographicSize *= zoomFactor; // Apply zoom factor
    }

    public void ResetZoom()
    {
        cam.orthographicSize = originalZoom; // Reset to original zoom
    }
}
