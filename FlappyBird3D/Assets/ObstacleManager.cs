using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Assign your 9 prefab variants here
    public Transform spawnPoint;
    public float spawnRate = 2f;
    public float moveSpeed = 5f;

    private List<GameObject> activeObstacles = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 0f, spawnRate);
    }

    void SpawnObstacle()
    {
        // 1. Pick a random obstacle variant
        int rand = Random.Range(0, obstaclePrefabs.Length);

        // 2. Spawn the obstacle at EXACTLY the spawn point's position and rotation
        GameObject obstacle = Instantiate(obstaclePrefabs[rand], spawnPoint.position, spawnPoint.rotation);

        // 3. Change color of all child renderers
        Renderer[] renderers = obstacle.GetComponentsInChildren<Renderer>();
        foreach (var rend in renderers)
        {
            rend.material.color = Random.ColorHSV(); // Gives nice contrast
        }

        // 4. Add movement forward (we'll assume forward is -Z for now)
        obstacle.AddComponent<ObstacleMover>().moveSpeed = moveSpeed;

        // 5. Track for cleanup
        activeObstacles.Add(obstacle);
    }
}
