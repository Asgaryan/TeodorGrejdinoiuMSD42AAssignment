using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] WaveConfig waveConfig;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();

        // Setting the starting position of the obstacle to the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ObstacleMove();
    }

    // Setting up waveConfig
    public void SetWaveConfig(WaveConfig waveConfigToSet)
    {
        waveConfig = waveConfigToSet;
    }

    void ObstacleMove()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            //save the current waypoint in targetPosition
            //targetPosition: where we want to go
            var targetPosition = waypoints[waypointIndex].transform.position;

            //to make sure z position is 0
            targetPosition.z = 0f;

            var movementThisFrame = waveConfig.GetObstacleMoveSpeed() * Time.deltaTime;
            //move from the current position, to the target position, the maximum distance one can move
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            //if we reached the target waypoint
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        //if obstacle moved to all waypoints
        else
        {
            Destroy(gameObject);
        }
    }
}
