using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeometryTest : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    public float meshScale = 1f;
    public float meshHeight = 2f;

    private void Start()
    {
        CreateMesh();
    }

    private void Update()
    {
        UpdateMesh();
    }

    private void CreateMesh()
    {
        mesh = new Mesh();
        

        vertices = new Vector3[4];
        triangles = new int[6];

        // Define vertices
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(0, meshHeight, 0);
        vertices[2] = new Vector3(meshScale, 0, 0);
        vertices[3] = new Vector3(meshScale, meshHeight, 0);

        // Define triangles (clockwise order)
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 2;
        triangles[4] = 1;
        triangles[5] = 3;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        GetComponent<MeshFilter>().mesh = mesh;

    }

    private void UpdateMesh()
    {
        // Modify vertices dynamically (for example, based on time or user input)
        float yOffset = Mathf.Sin(Time.time) * 0.5f; // Oscillate the mesh height
        vertices[1].y = meshHeight + yOffset;
        vertices[3].y = meshHeight + yOffset;

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}
