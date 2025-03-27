using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    public List<GameObject> buildingPrefabs;  // Assign 10 buildings in the inspector
    private Transform spawnPoint;    // Set a spawn point on the side of the road
    public float spacing = 10f;  // Adjustable spacing between buildings

    private List<int> buildingOrder;

    void Start()
    {
        // Find the first road segment in the scene and get its EndPoint
        GameObject road = GameObject.FindWithTag("Road");
        if (road != null)
        {
            spawnPoint = road.transform.Find("BuildingSpawnPoint");
        }

        GenerateRandomBuildingOrder();
        SpawnBuildings();
    }


    void GenerateRandomBuildingOrder()
    {
        buildingOrder = new List<int>();
        for (int i = 0; i < buildingPrefabs.Count; i++)
        {
            buildingOrder.Add(i);
        }
        Shuffle(buildingOrder);
    }

    void SpawnBuildings()
    {
        Vector3 spawnPos = spawnPoint.position;

        foreach (int index in buildingOrder)
        {
            GameObject building = Instantiate(buildingPrefabs[index], spawnPos, Quaternion.identity);
            spawnPos += new Vector3(0, 0, spacing);
        }
    }

    void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
