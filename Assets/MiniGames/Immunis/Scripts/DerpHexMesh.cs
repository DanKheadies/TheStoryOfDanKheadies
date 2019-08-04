using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class DerpHexMesh : MonoBehaviour
{
    Mesh derpHexMesh;
    List<Vector3> derpVertices;
    List<int> derpTriangles;
    MeshCollider derpMeshCollider;

    List<Color> derpColors;

    void Awake()
    {
        GetComponent<MeshFilter>().mesh = derpHexMesh = new Mesh();
        derpHexMesh.name = "Hex Mesh";
        derpVertices = new List<Vector3>();
        derpTriangles = new List<int>();
        derpMeshCollider = gameObject.AddComponent<MeshCollider>();
        derpColors = new List<Color>();
    }

    public void DerpTriangulate(DerpHexCell[] derpCells)
    {
        derpHexMesh.Clear();
        derpVertices.Clear();
        derpTriangles.Clear();
        derpColors.Clear();
        for (int i = 0; i < derpCells.Length; i++)
        {
            DerpTriangulate(derpCells[i]);
        }
        derpHexMesh.vertices = derpVertices.ToArray();
        derpHexMesh.colors = derpColors.ToArray();
        derpHexMesh.triangles = derpTriangles.ToArray();
        derpHexMesh.RecalculateNormals();

        derpMeshCollider.sharedMesh = derpHexMesh;
    }

    void DerpTriangulate(DerpHexCell derpCell)
    {
        Vector3 derpCenter = derpCell.transform.localPosition;
        //DerpAddTriangle(
        //    derpCenter,
        //    derpCenter + DerpHexMetrics.corners[0],
        //    derpCenter + DerpHexMetrics.corners[1]
        //);

        for (int i = 0; i < 6; i++)
        {
            DerpAddTriangle(
                derpCenter,
                derpCenter + DerpHexMetrics.corners[i],
                derpCenter + DerpHexMetrics.corners[i + 1]
            );
            DerpAddTriangleColor(derpCell.derpColor);
        }
    }

    void DerpAddTriangleColor(Color color)
    {
        derpColors.Add(color);
        derpColors.Add(color);
        derpColors.Add(color);
    }

    void DerpAddTriangle(Vector3 dv1, Vector3 dv2, Vector3 dv3)
    {
        int derpVertexIndex = derpVertices.Count;
        derpVertices.Add(dv1);
        derpVertices.Add(dv2);
        derpVertices.Add(dv3);
        derpTriangles.Add(derpVertexIndex);
        derpTriangles.Add(derpVertexIndex + 1);
        derpTriangles.Add(derpVertexIndex + 2);
    }
}