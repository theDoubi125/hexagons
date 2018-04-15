using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCell : MonoBehaviour
{
    public Cell cell;

    public void SetHeight(float height)
    {
        transform.localScale = new Vector3(1, height, 1);
        GetMapData().SetHeight(cell, height);
    }

    public MapData GetMapData()
    {
        return GetComponentInParent<MapGenerator>().map;
    }
}
