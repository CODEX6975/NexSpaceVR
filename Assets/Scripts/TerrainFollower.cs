using UnityEngine;

public class TerrainFollower : MonoBehaviour
{
    public Transform car;
    private Terrain terrain;

    void Start()
    {
        terrain = GetComponent<Terrain>();
        if (terrain == null)
        {
            Debug.LogError("No Terrain component found on this GameObject.");
        }
    }

    void LateUpdate()
    {
        if (car == null || terrain == null) return;

        // Get terrain size
        Vector3 terrainSize = terrain.terrainData.size;

        // Offset terrain position so its center matches car position
        float offsetX = -terrainSize.x * 0.5f;
        float offsetZ = -terrainSize.z * 0.5f;

        transform.position = new Vector3(car.position.x + offsetX, transform.position.y, car.position.z + offsetZ);
    }
}
