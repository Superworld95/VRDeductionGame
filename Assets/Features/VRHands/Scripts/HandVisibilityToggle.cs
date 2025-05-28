using UnityEngine;

public class HandVisibilityToggle : MonoBehaviour
{
    private SkinnedMeshRenderer handRenderer;
    private OVRGrabber grabber;

    private void Start()
    {
        // This script should be on the controller (with OVRGrabber)
        grabber = GetComponent<OVRGrabber>();

        // Assuming your hand mesh is a child of this GameObject
        handRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (grabber == null)
            Debug.LogError("OVRGrabber not found on this GameObject.");
        if (handRenderer == null)
            Debug.LogError("SkinnedMeshRenderer (hand model) not found in children.");
    }

    private void Update()
    {
        if (grabber.grabbedObject != null)
        {
            // Hide hand model when grabbing
            if (handRenderer.enabled)
                handRenderer.enabled = false;
        }
        else
        {
            // Show hand model when not grabbing
            if (!handRenderer.enabled)
                handRenderer.enabled = true;
        }
    }
}
