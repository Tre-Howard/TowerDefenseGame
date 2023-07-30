using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaypointSystem : MonoBehaviour // waypoint system for enemies to follow
{
    public List<Vector3> waypoints = new List<Vector3>(); // list of waypoints using Vector3 (positions)

    // Add a waypoint to the list - will use this later when "maps/levels" change and need to update the pathing mid game, right now it is not used
    public void AddWaypoint(Vector3 waypoint)
    {
        waypoints.Add(waypoint);
    }

    // Get the next waypoint in the list
    public Vector3 GetNextWaypoint(Vector3 currentWaypoint) // once the enemy reaches the waypoint, get next one and move to it
    {
        int currentIndex = waypoints.IndexOf(currentWaypoint); // get current waypoint
        if (currentIndex < waypoints.Count - 1) // if its less than the last waypoint, add one so enemy walks to next spot
        {
            return waypoints[currentIndex + 1];
        }
        else
        {
            return currentWaypoint; // Stay at the last waypoint - right now it repeats, but will change this to remove player health/lives
        }
    }

#if UNITY_EDITOR // all of this is for the #WaypointEditor script, its just to be used to visually see the path the enemies take in the level
    public bool showGizmos = true; // turn this to false to hide it during playtesting

    private void OnDrawGizmos()
    {
        if (showGizmos && Handles.ShouldRenderGizmos())
        {
            // Draw gizmos when not in play mode
            DrawWaypointGizmos();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (showGizmos && Handles.ShouldRenderGizmos())
        {
            // Draw gizmos when selected and not in play mode
            DrawWaypointGizmos();
        }
    }

    private void DrawWaypointGizmos()
    {
        Handles.color = Color.cyan;

        for (int i = 0; i < waypoints.Count; i++)
        {
            Vector3 currentWaypointPoint = waypoints[i];
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, Quaternion.identity, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 16;
            textStyle.normal.textColor = Color.white;
            Vector3 textAlligment = Vector3.down * 0.35f + Vector3.right * 0.35f;
            Handles.Label(waypoints[i] + textAlligment, $"{i + 1}", textStyle);

            if (currentWaypointPoint != newWaypointPoint)
            {
                Undo.RecordObject(this, "Move Waypoint");
                waypoints[i] = newWaypointPoint;
            }

            if (i > 0)
            {
                Handles.DrawLine(waypoints[i - 1], waypoints[i]);
            }
        }
    }
#endif
}
