using UnityEngine;
using UnityEngine.XR;

public class VRCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        if (XRSettings.enabled)
        {
            Debug.Log("VR is enabled, disabling manual camera control");
            enabled = false;
        }
    }

    void Update()
    {
        if (XRSettings.enabled) return; // Prevents manual control if VR is active

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevents flipping

        yRotation += mouseX;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
