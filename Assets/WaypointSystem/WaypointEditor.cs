using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaypointSystem))] // custom editor for waypoint system
public class WaypointEditor : Editor // short version: creates draggable nodes that have lines connecting to show how enemies paths, makes it easier to create levels
{
    private WaypointSystem waypointSystem => target as WaypointSystem;

    private void OnSceneGUI()
    {
        if (waypointSystem.waypoints == null || waypointSystem.waypoints.Count == 0)
            return;

        Handles.color = Color.cyan;

        for (int i = 0; i < waypointSystem.waypoints.Count; i++)
        {
            EditorGUI.BeginChangeCheck();

            // Create handles
            Vector3 currentWaypointPoint = waypointSystem.waypoints[i];
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, Quaternion.identity, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

            // Create text
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 16;
            textStyle.normal.textColor = Color.white;
            Vector3 textAlligment = Vector3.down * 0.35f + Vector3.right * 0.35f;
            Handles.Label(waypointSystem.waypoints[i] + textAlligment, $"{i + 1}", textStyle);
            EditorGUI.EndChangeCheck();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Move Handle");
                waypointSystem.waypoints[i] = newWaypointPoint;
            }
        }
    }
}
