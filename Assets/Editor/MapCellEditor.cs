using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapCell))]
[CanEditMultipleObjects]
public class MapCellEditor : Editor
{
    Tool lastTool;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }

    public void OnSceneGUI()
    {
        MapCell example = (MapCell)target;

        EditorGUI.BeginChangeCheck();
        Vector3 newTargetPosition = Handles.PositionHandle(example.transform.position + Vector3.up * example.transform.localScale.y , Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(example, "Change height of cell (" + example.cell.x + ", " + example.cell.y + ")");
            example.SetHeight(newTargetPosition.y - example.transform.position.y);
            EditorUtility.SetDirty(example.GetMapData());
            AssetDatabase.SaveAssets();
        }
    }

    void OnEnable()
    {
        lastTool = Tools.current;
        Tools.current = Tool.None;
    }

    void OnDisable()
    {
        Tools.current = lastTool;
    }
}
