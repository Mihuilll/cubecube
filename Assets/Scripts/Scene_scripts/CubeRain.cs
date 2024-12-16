using UnityEngine;

public class CubeRain : MonoBehaviour
{ 
    public GameObject cubePrefab; 
    public Vector3 spawnAreaCenter; 
    public Vector3 spawnAreaSize; 
    public float spawnInterval = 0.5f; 
    public float destroyDelay = 5f;

    private void Start()
    { 
        InvokeRepeating(nameof(SpawnCube), 0f, spawnInterval);
    }

    private void SpawnCube()
    { 
        Vector3 spawnPosition = spawnAreaCenter + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        ); 
        GameObject cube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity); 
        Destroy(cube, destroyDelay);
    }

    private void OnDrawGizmos()
    { 
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
    }
}
