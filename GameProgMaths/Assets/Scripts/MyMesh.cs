using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMesh : MonoBehaviour
{
    public int segments = 32; // Number of segments in the donut
    public float innerRadius = 1.0f; // Inner radius of the donut
    public float outerRadius = 2.0f; // Outer radius of the donut

    private void OnValidate()
    {
        GenerateDonut();
    }
    private void Awake()
    {
        GenerateDonut();
    }

    void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>
        {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0)
        };
        List<int> triangles = new List<int> { 0, 2, 1, 1, 2, 3 };

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);

        // Attach the mesh to a GameObject
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

    }

    private void GenerateDisk()
    {
        Mesh mesh = new Mesh();

        Vector3 center = transform.position;

        List<Vector3> vertices = new List<Vector3>();

        for (int i = 0; i < segments - 1; i++)
        {
            float x = Mathf.Cos((float)i / (float)segments * 2f * Mathf.PI);
            float y = Mathf.Sin((float)i / (float)segments * 2f * Mathf.PI);
            vertices.Add(new Vector3 (x, y, 0f));
        }

        List<int> tris = new List<int>();
        for(int i = 0;i < segments; i++)
        {
            tris.Add(0);
            tris.Add(i + 1);
            tris.Add((i + 2));
        }
        tris.Add(0);
        tris.Add(segments);
        tris.Add(1);

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    private void GenerateDonut()
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < segments; i++)
        {
            float angle = 2 * Mathf.PI * i / segments;
            Vector3 innerPoint = new Vector3(innerRadius * Mathf.Cos(angle), innerRadius * Mathf.Sin(angle), 0);
            Vector3 outerPoint = new Vector3(outerRadius * Mathf.Cos(angle), outerRadius * Mathf.Sin(angle), 0);

            vertices.Add(innerPoint);
            vertices.Add(outerPoint);

            int nextIndex = (i + 1) % segments;
            triangles.Add(i * 2);
            triangles.Add(nextIndex * 2);
            triangles.Add(i * 2 + 1);

            triangles.Add(i * 2 + 1);
            triangles.Add(nextIndex * 2);
            triangles.Add(nextIndex * 2 + 1);

            uvs.Add(innerPoint);
            uvs.Add(outerPoint + innerPoint);
        }


        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetUVs(0, uvs);
        
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        
    }

private void OnDrawGizmos()
    {
        
        for (int i = 0; i < segments; i++)
        {
            Vector2 pos;
            pos.x = Mathf.Cos((float)i / (float)segments * 2f * Mathf.PI);
            pos.y = Mathf.Sin((float)i / (float)segments * 2f * Mathf.PI);
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
