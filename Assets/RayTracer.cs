using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A cell in cube coordinates
public struct Cell
{
    public Cell(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int x;
    public int y;
    public int z { get { return -x - y; } }
}

public class RayTracer : MonoBehaviour
{
    public float precision = 0.1f;
    public float cellSize = 1;
    public GameObject cursor;
    public GameObject cursor2;
    public float cursorHeight = 5;

    Cell RaytraceCell(Ray ray)
    {
        Cell result = new Cell();
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, 100))
        {
            cursor2.transform.position = hitInfo.point;
            Vector2 point = new Vector2(hitInfo.point.x, hitInfo.point.z);
            point -= new Vector2(ray.direction.x, ray.direction.z).normalized * precision;
            Vector2 cellpos = new Vector2();
            cellpos.x = (point.x * Mathf.Sqrt(3) / 3 - point.y / 3) / cellSize;
            cellpos.y = point.y * 2 / 3 / cellSize;
            result = RoundCellPos(cellpos);
        }
        return result;
    }

    Cell RoundCellPos(Vector2 pos)
    {
        float x = pos.x;
        float y = pos.y;
        float z = -x - y;
        float rx = Mathf.Round(x);
        float ry = Mathf.Round(y);
        float rz = Mathf.Round(z);
        float xdiff = Mathf.Abs(x - rx);
        float ydiff = Mathf.Abs(y - ry);
        float zdiff = Mathf.Abs(z - rz);
        if (xdiff > ydiff && xdiff > zdiff)
        {
            rx = -ry - rz;
        }
        else if (ydiff > zdiff)
        {
            ry = -rx - rz;
        }
        else rz = -rx - ry;
        Cell result = new Cell((int)rx, (int)ry);
        return result;
    }

    public static Vector3 GetCellPosition(Cell cell)
    {
        return new Vector3(Mathf.Sqrt(3) * (cell.x + cell.y / 2.0f), 0, cell.y * 1.5f);
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ray.direction.Normalize();
        ray.direction *= 100;
        Cell cell = RaytraceCell(ray);
        Vector3 pos = GetCellPosition(cell);
        pos.y = cursorHeight;
        RaycastHit raycastHit;
        if (Physics.Raycast(pos, Vector3.down, out raycastHit))
        {
            cursor.transform.position = raycastHit.point;
        }
	}
}
