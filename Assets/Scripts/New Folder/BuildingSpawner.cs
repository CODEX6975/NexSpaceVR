using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    public GameObject[] buildingPrefabs;  // Array of building prefabs to spawn
    public Transform[] roadSegments;      // Road segments to spawn buildings alongside
    public float minDistance = 10f;       // Minimum distance between buildings to avoid overlap
    public float maxDistance = 20f;       // Maximum distance between buildings
    public float sideOffset = 15f;        // Distance from the center of the road to spawn buildings
    public int buildingsPerSegment = 3;   // How many buildings to spawn per road segment

    void Start()
    {
        SpawnBuildings();
    }

    public void SpawnBuildings()
    {
        foreach (Transform road in roadSegments)
        {
            float spawnZ = road.position.z - road.localScale.z / 2;  // Start from the beginning of the road
            float lastSpawnZ = spawnZ;  // Track the last spawn position

            Debug.Log("Spawning buildings on road: " + road.name);

            for (int i = 0; i < buildingsPerSegment; i++)
            {
                // Randomize the distance between buildings, but make sure they don't overlap
                spawnZ = lastSpawnZ + Random.Range(minDistance, maxDistance);
                lastSpawnZ = spawnZ;

                // Randomly decide whether the building will be on the left or right side of the road
                float sideDirection = Random.value > 0.5f ? 1 : -1;  // Randomly pick left or right

                // Spawn the building at the correct position, parallel to the road
                Vector3 spawnPosition = new Vector3(road.position.x + sideDirection * sideOffset, 0, spawnZ);

                Debug.Log("Spawning building at position: " + spawnPosition); // Log spawn position

                // Pick a random building prefab
                GameObject building = Instantiate(
                    buildingPrefabs[Random.Range(0, buildingPrefabs.Length)],
                    spawnPosition,
                    Quaternion.Euler(-90f, 0f, 0f) // Set the X rotation to -90 degrees, Y and Z rotation remain unchanged
                );

                // Optionally, add the building as a child of the road segment (for better organization)
                building.transform.parent = road;
            }
        }
    }
}
