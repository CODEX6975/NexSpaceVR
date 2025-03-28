using UnityEngine;

public class CarManager : MonoBehaviour
{
    public GameObject carPrefab;  // Assign the Car prefab in the Inspector
    private GameObject carInstance;

    void Start()
    {
        SpawnCar();
    }

    void SpawnCar()
    {
        if (carPrefab == null)
        {
            Debug.LogError("Car prefab is missing! Assign it in the Inspector.");
            return;  // Stop execution to prevent the error
        }

        Debug.Log("Spawning car: " + carPrefab.name);
        carInstance = Instantiate(carPrefab, Vector3.zero, Quaternion.identity);

        // Optional: Parent the XR Rig to the new car instance
        Transform xrRig = carInstance.transform.Find("XR Rig");
        if (xrRig != null)
        {
            xrRig.SetParent(carInstance.transform);
        }
    }
}
