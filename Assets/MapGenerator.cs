using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour
{
    public int minX = -10;
    public int maxX = 10;
    public int minY = -10;
    public int maxY = 10;
    public Transform cellPrefab;

    public MapData map;

    public bool mustUpdate = true;
    
	void Start ()
    {
        GenerateMap();

    }
	
	void Update ()
    {
        if (!Application.isPlaying && mustUpdate)
        {
            GenerateMap();
            mustUpdate = false;
        }
	}

    void GenerateMap()
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }
        foreach(Transform child in children)
        {
            if (Application.isPlaying)
                Destroy(child.gameObject);
            else DestroyImmediate(child.gameObject);
        }
        if (cellPrefab != null)
        {
            for (int i = 0; i < map.w; i++)
            {
                for (int j = 0; j < map.h; j++)
                {
                    Transform instance = Instantiate<Transform>(cellPrefab, RayTracer.GetCellPosition(new Cell(i, j)), Quaternion.identity, transform);
                    instance.transform.localScale = new Vector3(1, Mathf.Max(0.01f, map.GetHeight(i, j)), 1);
                    instance.transform.localRotation = Quaternion.Euler(new Vector3(0, 30, 0));
                    instance.GetComponent<MapCell>().cell = new Cell(i, j);
                }
            }
        }
    }

    public void SetHeight(Cell cell, float height)
    {
        map.SetHeight(cell, height);
    }
}
