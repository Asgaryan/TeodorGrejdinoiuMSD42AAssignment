using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // A list of waves
    [SerializeField] bool looping = true;
    [SerializeField] List<WaveConfig> waveConfigList;

    // Bullet prefab
    [SerializeField] GameObject bulletPrefab;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            //start the coroutine that spawns all obstacles in our currentWave
            yield return StartCoroutine(SpawnAllWaves());
        }
        //when coroutine SpawnAllWaves finishes check if looping == true
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        // Loop from starting position to end position in our list
        foreach (WaveConfig currentWave in waveConfigList)
        {
            yield return StartCoroutine(SpawnAllObstaclesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllObstaclesInWave(WaveConfig waveToSpawn)
    {
        // Checks if the wave spawns a bullet
        if (waveToSpawn.GetSpawnBullet())
        {
            // Create bullet instance
            var newBullet = Instantiate(
                    bulletPrefab,
                    new Vector3(waveToSpawn.GetBulletX(), 6, 0),
                    Quaternion.identity);

            // Add velocity to bullet
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -15f);
        }

        for (int obstacleCount = 0; obstacleCount < waveToSpawn.GetObstacleQuantity(); obstacleCount++)
        {
            // Spawn the obstaclePrefab from waveToSpawn
            // at the position specified waveToSpawn waypoints
            var newObstacle = Instantiate(
                        waveToSpawn.GetObstaclePrefab(),
                        waveToSpawn.GetWaypoints()[0].transform.position,
                        Quaternion.identity);

            // Select the wave and apply the obstacle to it
            newObstacle.GetComponent<ObstaclePathing>().SetWaveConfig(waveToSpawn);

            // wait spawnInterval
            yield return new WaitForSeconds(waveToSpawn.GetSpawnInterval());
        }
    }

}
