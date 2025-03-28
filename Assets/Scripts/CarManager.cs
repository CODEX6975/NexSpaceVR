using UnityEngine;

public class CarManager : MonoBehaviour
{
    public GameObject carPrefab;  // Assign the Car prefab in the Inspector
    private GameObject carInstance;
    public float carSpeed = 5f;  // Set this to control car's movement speed

    void Start()
    {
        SpawnCar();
    }

    void Update()
    {
        if (carInstance != null)
        {
            // Move the car backward along the Z-axis (negative direction)
            carInstance.transform.position += new Vector3(0, 0, -carSpeed * Time.deltaTime);
        }
    }

    void SpawnCar()
    {
        if (carPrefab == null)
        {
            Debug.LogError("Car prefab is missing! Assign it in the Inspector.");
            return;  // Stop execution to prevent the error
        }

        Debug.Log("Spawning car: " + carPrefab.name);

        // Position the car just above the road (y = 1.5)
        carInstance = Instantiate(carPrefab, new Vector3(0, 1.5f, 0), Quaternion.identity);

        // Rotate the entire car prefab to face the negative Z-direction (backwards)
        // This rotates the whole car to face negative Z (backwards)
        carInstance.transform.rotation = Quaternion.Euler(0, 0, 0);  // Rotate 180 degrees on the Y-axis to face -Z

        // Optional: Parent the XR Rig to the new car instance (if applicable)
        Transform xrRig = carInstance.transform.Find("XR Rig");
        if (xrRig != null)
        {
            xrRig.SetParent(carInstance.transform);
        }
    }
}
