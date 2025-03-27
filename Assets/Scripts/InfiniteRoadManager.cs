using System.Collections.Generic;
using UnityEngine;

public class InfiniteRoadManager : MonoBehaviour
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
        if (player.position.z > activeRoads.Peek().transform.position.z + 10f) // Adjust offset as needed
        {
            DespawnOldestRoad();
            SpawnRoad();
        }
    }

    void SpawnRoad()
    {
        GameObject newRoad = Instantiate(roadPrefab, nextSpawnPosition, Quaternion.identity);
        activeRoads.Enqueue(newRoad);

        // Move spawn position forward
        nextSpawnPosition = newRoad.transform.Find("EndPoint").position;
    }

    void DespawnOldestRoad()
    {
        GameObject oldRoad = activeRoads.Dequeue();
        Destroy(oldRoad);
    }
}
