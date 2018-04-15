using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "MapData", order = 1)]
public class MapData : ScriptableObject
{
    public int w, h;
    public float[] heights;

    public void SetSize(int newW, int newH)
    {
        if (heights == null || heights.Length != w * h)
            heights = null;
        float[] oldHeights = heights;
        heights = new float[newW * newH];
        if(oldHeights != null)
        {
            for (int i = 0; i < Mathf.Min(w, newW); i++)
            {
                for (int j = 0; j < Mathf.Min(h, newH); j++)
                {
                    heights[i + newW * j] = oldHeights[i + w * j];
                }
            }
        }

        w = newW;
        h = newH;
    }

    public float GetHeight(int x, int y)
    {
        if (x > w || y > h || heights == null)
            return 0;
        return heights[x + w * y];
    }

    public void SetHeight(Cell cell, float height)
    {
        heights[cell.x + w * cell.y] = height;
    }
    
}
