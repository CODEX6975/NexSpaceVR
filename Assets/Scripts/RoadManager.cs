using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject roadPrefab;  // Assign your road prefab in the inspector
    public Transform player;  // Reference to the player (car)
    private Queue<GameObject> activeRoads = new Queue<GameObject>();

    private Vector3 nextSpawnPosition = Vector3.zero;

    void Start()
    {
        // Spawn initial three road pieces
        for (int i = 0; i < 3; i++)
        {
            SpawnRoad();
        }
    }

    void Update()
    {
        // Check if player has passed the second road piece
        if (player.position.z > activeRoads.Peek().transform.position.z + 20f) // Adjust this offset to suit the road size
        {
            DespawnOldestRoad();
            SpawnRoad();
        }
    }

    void SpawnRoad()
    {
        // Instantiate a new road piece at the next spawn position
        GameObject newRoad = Instantiate(roadPrefab, nextSpawnPosition, Quaternion.identity);
        activeRoads.Enqueue(newRoad);

        // Move spawn position forward to the next road segment's end
        Transform roadEndPoint = newRoad.transform.Find("EndPoint"); // Ensure EndPoint is correctly named in the prefab

        // If EndPoint exists, set the next spawn position to the EndPoint's position
        if (roadEndPoint != null)
        {
            nextSpawnPosition = roadEndPoint.position;
        }
        else
        {
            Debug.LogError("EndPoint not found in road prefab! Please ensure EndPoint GameObject is in the prefab.");
        }

        // Optional: Log the spawn position to the console for debugging
        Debug.Log("Spawned Road at: " + newRoad.transform.position);
    }

    void DespawnOldestRoad()
    {
        GameObject oldRoad = activeRoads.Dequeue();
        Destroy(oldRoad);
    }

    // This method will draw a gizmo to represent the next spawn position in the scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;  // Choose a color for the gizmo
        Gizmos.DrawSphere(nextSpawnPosition, 1f);  // Draw a sphere at the next spawn position with a radius of 1
    }
}
