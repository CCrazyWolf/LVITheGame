using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class MapMesh : MonoBehaviour
{
    Mesh mesh;

    List<Vector3> vertices;
    List<int> triangles;

    public bool requiresCollider = false;
    MeshCollider colli;

    private void Awake()
    {
        mesh = new Mesh();
        mesh.name = "Meshh";
        GetComponent<MeshFilter>().mesh = mesh;
        vertices = new List<Vector3>();
        triangles = new List<int>();

        if (requiresCollider)
        {
            colli = gameObject.AddComponent<MeshCollider>();
            colli.sharedMesh = this.mesh;
        }
    }

    public void Clear()
    {
        mesh.Clear();
        vertices.Clear();
        triangles.Clear();
    }

    public void Apply()
    {
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        colli.sharedMesh = mesh;
    }

    public void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int counter = vertices.Count;

        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);

        triangles.Add(counter);
        triangles.Add(counter+1);
        triangles.Add(counter+2);
    }

    public void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
    {
        int counter = vertices.Count;

        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        vertices.Add(v4);

        triangles.Add(counter);
        triangles.Add(counter + 2);
        triangles.Add(counter + 1);
        triangles.Add(counter + 1);
        triangles.Add(counter + 2);
        triangles.Add(counter + 3);
    }    
}
