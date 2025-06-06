using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Cache the main camera at start for better performance
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        // Make the canvas face the camera
        transform.forward = mainCamera.transform.forward;
    }
}
