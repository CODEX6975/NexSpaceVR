using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject roadPrefab; // Road segment prefab
    public GameObject[] buildingPrefabs; // Array of building prefabs
    public int buildingsPerSide = 5; // Number of buildings per side
    public float roadLength = 50f; // Length of each road segment
    public Transform player; // Reference to the player's position

    private Queue<GameObject> activeRoads = new Queue<GameObject>(); // Track spawned roads

    void Start()
    {
        // Spawn initial 3 road segments
        for (int i = 0; i < 3; i++)
        {
            SpawnRoad(i * roadLength);
        }
    }

    void Update()
    {
        // Check if the player is near the next segment spawn trigger
        if (player.position.z > activeRoads.Peek().transform.position.z + roadLength)
        {
            RecycleRoad();
        }
    }

    void SpawnRoad(float zPosition)
    {
        // Spawn road segment
        GameObject newRoad = Instantiate(roadPrefab, new Vector3(0, 0, zPosition), Quaternion.identity);
        activeRoads.Enqueue(newRoad);

        // Spawn buildings along the road
        SpawnBuildings(newRoad.transform, zPosition);

        // If more than 3 segments exist, remove the oldest one
        if (activeRoads.Count > 3)
        {
            GameObject oldRoad = activeRoads.Dequeue();
            Destroy(oldRoad);
        }
    }

    void SpawnBuildings(Transform roadTransform, float zPosition)
    {
        float spacing = roadLength / buildingsPerSide; // Equal spacing per segment

        for (int i = 0; i < buildingsPerSide; i++)
        {
            float xLeft = -10f; // Left side position
            float xRight = 10f; // Right side position
            float zOffset = i * spacing - (roadLength / 2); // Align buildings along road

            Vector3 leftPosition = new Vector3(xLeft, 0, zPosition + zOffset);
            Vector3 rightPosition = new Vector3(xRight, 0, zPosition + zOffset);

            GameObject leftBuilding = Instantiate(GetRandomBuilding(), leftPosition, Quaternion.identity, roadTransform);
            GameObject rightBuilding = Instantiate(GetRandomBuilding(), rightPosition, Quaternion.identity, roadTransform);

            leftBuilding.transform.LookAt(new Vector3(leftBuilding.transform.position.x + 1, 0, leftBuilding.transform.position.z));
            rightBuilding.transform.LookAt(new Vector3(rightBuilding.transform.position.x - 1, 0, rightBuilding.transform.position.z));
        }
    }

    GameObject GetRandomBuilding()
    {
        return buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
    }

    void RecycleRoad()
    {
        float newZ = activeRoads.Peek().transform.position.z + roadLength * 3; // Move to front
        SpawnRoad(newZ);
    }
}
