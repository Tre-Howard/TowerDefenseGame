using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour // script for enemy movement
{
    public EnemyStats enemyStats; // get movement speed from enemyStats
    public WaypointSystem waypointSystem; // which waypoint system this enemy will follow

    private int currentWaypointIndex; // current waypoint in the waypointSystem

    private void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        // Start at the first waypoint
        currentWaypointIndex = 0; // begin at the start of the waypoint list
    }

    private void Update()
    {
        if (waypointSystem == null || waypointSystem.waypoints.Count == 0)
        {
            // No waypoints available, do nothing - change this later to destroy self or something else
            return;
        }

        // Move towards the current waypoint
        Vector3 currentWaypointPosition = waypointSystem.waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentWaypointPosition, enemyStats.movementSpeed * Time.deltaTime);

        // Check if the enemy reached the current waypoint
        if (transform.position == currentWaypointPosition)
        {
            // Get the next waypoint index
            currentWaypointIndex = (currentWaypointIndex + 1) % waypointSystem.waypoints.Count;
        }
    }
}
