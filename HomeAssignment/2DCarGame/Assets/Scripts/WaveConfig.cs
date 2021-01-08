using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Obstacle Wave Config")]
public class WaveConfig : ScriptableObject
{
    // The obstacle to spawn
    [SerializeField] GameObject obstaclePrefab;

    // The path used by obstacles
    [SerializeField] GameObject pathPrefab;

    // Obstacle movement speed
    [SerializeField] float obstacleMoveSpeed = 2f;

    // Number of obstacles in a wave
    [SerializeField] int obstacleQuantity = 5;

    // Time between each obstacle spawn
    [SerializeField] float spawnInterval = 1f;

    // Does the obstacle wave spawn a bullet?
    [SerializeField] bool spawnBullet = false;

    // Where is the bullet spawned on the x-axis
    [SerializeField] float bulletX = 0f;

    // Encapsulation
    public GameObject GetObstaclePrefab()
    {
        return obstaclePrefab;
    }

    public List<Transform> GetWaypoints()
    {
        // Each wave can have different number of waypoints
        var waveWaypoints = new List<Transform>();

        // Go into Path Prefab and add each child to the list
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }

    public float GetObstacleMoveSpeed()
    {
        return obstacleMoveSpeed;
    }

    public int GetObstacleQuantity()
    {
        return obstacleQuantity;
    }

    public float GetSpawnInterval()
    {
        return spawnInterval;
    }

    public bool GetSpawnBullet()
    {
        return spawnBullet;
    }

    public float GetBulletX()
    {
        return bulletX;
    }
}
