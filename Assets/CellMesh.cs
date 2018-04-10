using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CellMesh : MonoBehaviour
{
    public float radius = 1;
    public float height = 1;

    private static Mesh mesh;

    void Start()
    {
        if (mesh == null)
        {
            GenerateMesh();
        }

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void Update()
    {
        
    }

    void GenerateMesh()
    {
        mesh = new Mesh();
        Vector3[] vertices = new Vector3[(6 + 1) * 2 + 4 * 6];
        int[] topSidesTriangles = new int[] { 0, 1, 2,  0, 2, 3,    0, 3, 4,    0, 4, 5,    0, 5, 6,    0, 6, 1,
                                                8, 7, 9,  9, 7, 10,   10, 7, 11,  11, 7, 12,  12, 7, 13,  13, 7, 8};

        int[] triangles = new int[topSidesTriangles.Length + 3 * 2 * 6];
        for (int i = 0; i < topSidesTriangles.Length; i++)
            triangles[i] = topSidesTriangles[i];
        vertices[0] = Vector3.zero;
        vertices[7] = Vector3.up * height;
        for (int i = 0; i < 6; i++)
        {
            float angle = i * 2 * Mathf.PI / 6;
            float nextAngle = (i + 1) * 2 * Mathf.PI / 6;
            vertices[i + 1] = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
            vertices[i + 8] = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle)) + Vector3.up * height;

            vertices[i * 4 + 14] = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
            vertices[i * 4 + 14 + 1] = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle)) + Vector3.up * height;
            vertices[i * 4 + 14 + 2] = new Vector3(radius * Mathf.Cos(nextAngle), 0, radius * Mathf.Sin(nextAngle));
            vertices[i * 4 + 14 + 3] = new Vector3(radius * Mathf.Cos(nextAngle), 0, radius * Mathf.Sin(nextAngle)) + Vector3.up * height;
            triangles[topSidesTriangles.Length + i * 6] = i * 4 + 14;
            triangles[topSidesTriangles.Length + i * 6 + 1] = i * 4 + 14 + 1;
            triangles[topSidesTriangles.Length + i * 6 + 2] = i * 4 + 14 + 2;
            triangles[topSidesTriangles.Length + i * 6 + 3] = i * 4 + 14 + 2;
            triangles[topSidesTriangles.Length + i * 6 + 4] = i * 4 + 14 + 1;
            triangles[topSidesTriangles.Length + i * 6 + 5] = i * 4 + 14 + 3;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
