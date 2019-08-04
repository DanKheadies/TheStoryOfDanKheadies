// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: Jasper Flick
// Contributors: David W. Corso
// Start: 07/12/2019
// Last:  07/13/2019

using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    public Mesh hexMesh;
    public MeshCollider meshCollider;
    public List<Color> colors;
    public List<int> triangles;
    public List<Vector3> vertices;

    void Awake()
    {
        // Initializers
        colors = new List<Color>();
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        hexMesh.name = "Hex Mesh";
        meshCollider = gameObject.AddComponent<MeshCollider>();
        vertices = new List<Vector3>();
        triangles = new List<int>();
    }

    public void Triangulate(HexCell[] cells)
    {
        colors.Clear();
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        for (int i = 0; i < cells.Length; i++)
        {
            Triangulate(cells[i]);
        }
        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.RecalculateNormals();

        meshCollider.sharedMesh = hexMesh;
    }

    //void Triangulate(HexCell cell)
    //{
    //    Vector3 center = cell.transform.localPosition;
    //    for (int i = 0; i < 6; i++)
    //    {
    //        AddTriangle(
    //            center,
    //            center + HexMetrics.corners[i],
    //            center + HexMetrics.corners[i + 1]
    //        );
    //        AddTriangleColor(cell.color);
    //    }
    //}

    void Triangulate(HexCell cell)
    {
        for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        {
            Triangulate(d, cell);
        }
    }

    void Triangulate(HexDirection direction, HexCell cell)
    {
        Vector3 center = cell.transform.localPosition;
        AddTriangle(
            center,
            center + HexMetrics.GetFirstCorner(direction),
            center + HexMetrics.GetSecondCorner(direction)
        //center + HexMetrics.corners[(int)direction],
        //center + HexMetrics.corners[(int)direction + 1]
        );
        HexCell neighbor = cell.GetNeighbor(direction) ?? cell;
        Color edgeColor = (cell.color + neighbor.color) * 0.5f;
        AddTriangleColor(cell.color, edgeColor, edgeColor);
    }

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

    void AddTriangleColor(Color c1, Color c2, Color c3)
    {
        colors.Add(c1);
        colors.Add(c2);
        colors.Add(c3);
    }
}
