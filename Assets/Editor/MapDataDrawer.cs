using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapData))]
[CanEditMultipleObjects]
public class MapDataDrawer : Editor
{
    SerializedProperty w, h, heights;
    Vector2 ScrollPos;
    public void OnEnable()
    {
        w = serializedObject.FindProperty("w");
        h = serializedObject.FindProperty("h");
        heights = serializedObject.FindProperty("heights");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("w");
        int newW = EditorGUILayout.IntSlider(w.intValue, 1, 50);
        EditorGUILayout.LabelField("h");
        int newH = EditorGUILayout.IntSlider(h.intValue, 1, 50);

        if(newW != w.intValue || newH != h.intValue)
        {
            float[] oldHeights = new float[w.intValue * h.intValue];
            for (int i = 0; i < w.intValue; i++)
            {
                for (int j = 0; j < h.intValue; j++)
                {
                    if(i + j * w.intValue < heights.arraySize)
                        oldHeights[i + j * w.intValue] = heights.GetArrayElementAtIndex(i + j * w.intValue).floatValue;
                }
            }
            heights.arraySize = newW * newH;
            for (int i = 0; i < newW; i++)
            {
                for (int j = 0; j < newH; j++)
                {
                    if (i < w.intValue && j < h.intValue)
                        heights.GetArrayElementAtIndex(i + j * newW).floatValue = oldHeights[i + j * w.intValue];
                    else
                        heights.GetArrayElementAtIndex(i + j * newW).floatValue = 0;
                }
            }

        }

        ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos);

        for (int j = 0; j < newH; j++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < newW; i++)
            {
                if (i + j * newW < heights.arraySize)
                {
                    SerializedProperty heightProperty = heights.GetArrayElementAtIndex(i + j * newW);
                    if (heightProperty != null)
                    {
                        float height = heightProperty.floatValue;
                        float newHeight = EditorGUILayout.DelayedFloatField(height);
                        if (height != newHeight)
                            heights.GetArrayElementAtIndex(i + j * w.intValue).floatValue = newHeight;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
        w.intValue = newW;
        h.intValue = newH;
        serializedObject.ApplyModifiedProperties();
    }
}
