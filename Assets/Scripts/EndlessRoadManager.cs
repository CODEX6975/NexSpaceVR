using UnityEngine;

public class EndlessRoadManager : MonoBehaviour
{
    public GameObject[] roadSegments; // Holds the three road segments
    public float roadLength = 50f; // Length of each road segment
    public Transform car; // Reference to the car
    private float roadY = 0.5f; // Fixed Y position for road segments

    void Update()
    {
        // Prevent errors if roadSegments isn't set correctly
        if (roadSegments == null || roadSegments.Length < 3 || car == null)
        {
            Debug.LogError("EndlessRoadManager: roadSegments or car is not set properly in the Inspector!");
            return;
        }

        // Check if the car has reached the end of Segment 2
        float segment2End = roadSegments[1].transform.position.z + roadLength / 2;

        if (car.position.z > segment2End)
        {
            MoveRoadForward();
        }
    }

    void MoveRoadForward()
    {
        GameObject oldSegment = roadSegments[0];
        float newZ = roadSegments[2].transform.position.z + roadLength;

        // Always force Y to 0.5f
        oldSegment.transform.position = new Vector3(0, roadY, newZ);

        // Shift segments
        roadSegments[0] = roadSegments[1];
        roadSegments[1] = roadSegments[2];
        roadSegments[2] = oldSegment;

        // Remove old buildings and spawn new ones
        foreach (Transform child in oldSegment.transform)
        {
            Destroy(child.gameObject);
        }

        // Spawn new buildings
        FindFirstObjectByType<BuildingSpawner>().SpawnBuildings();

        Debug.Log("Road moved forward, buildings spawned!");
    }
}
