using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
[CanEditMultipleObjects]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }

    public void OnSceneGUI()
    {
        MapGenerator example = (MapGenerator)target;

        EditorGUI.BeginChangeCheck();
        Quaternion newTargetPosition = Handles.RotationHandle(Quaternion.identity, Vector3.zero);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(example, "Change Look At Target Position");
            example.transform.localRotation = newTargetPosition;
        }
    }
}
