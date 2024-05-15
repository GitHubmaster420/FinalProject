using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter))]


public class RoadSegment : MonoBehaviour
{
    public BezierPoint[] points;

    public bool ClosedPath = false;

    [SerializeField] float tTest = 0f;

    [SerializeField] Mesh2D shape2D;

    [Range(2, 64)]
    [SerializeField] int edgeRingCount = 10;

    Mesh mesh = null;



    private void OnValidate()
    {
        GenerateMesh();
    }

    void GenerateMesh()
    {

        if (mesh == null)
        {
            mesh = new Mesh();
        }

        else
        {
            mesh.Clear();
        }
        

        List<Vector3> verts = new List<Vector3>();

        for(int ring = 0; ring <  edgeRingCount; ring++) {

            float t = ring / (edgeRingCount - 1f);
            OrientedPoint op = GetBezierPoint(t);

            for(int i = 0; i < shape2D.VertexCount; i++)
            {
                verts.Add(op.LocalToWorldPos(shape2D.vertices[i].point));
            }
        }

        List<int> triIndices = new List<int>();
        for (int ring = 0; ring < edgeRingCount - 1; ring++)
        {
            int rootIndex = ring * shape2D.VertexCount;
            int rootIndexNext = (ring + 1) * shape2D.VertexCount;

            for (int line = 0; line < shape2D.LineCount; line += 2)
            {
                int lineIndexA = shape2D.lineIndices[line];
                int lineIndexB = shape2D.lineIndices[line + 1];

                int currentA = rootIndex + lineIndexA;
                int currentB = rootIndex + lineIndexB;
                int nextA = rootIndexNext + lineIndexA;
                int nextB = rootIndexNext + lineIndexB;

                triIndices.Add(currentA);
                triIndices.Add(nextA);
                triIndices.Add(nextB);

                triIndices.Add(currentA);
                triIndices.Add(nextB);
                triIndices.Add(currentB);
            }
        }

        mesh.SetVertices(verts);
        mesh.SetTriangles(triIndices, 0);
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    private int n
    {
        get { return points.Length; }
    }


    private void OnDrawGizmos()
    {

        int n = points.Length;
        for (int i = 0; i < n - 1; i++)
        {
            Vector3 first_anchor = points[i].getAnchorPoint();
            Vector3 second_anchor = points[i + 1].getAnchorPoint();

            Vector3 first_control = points[i].getSecondControlpoint();
            Vector3 second_control = points[i + 1].getFirstControlPoint();

            Handles.DrawBezier(first_anchor, second_anchor, first_control, second_control, Color.green, new Texture2D(1, 1), 1);
        }

        OrientedPoint testOrientedPoint = GetBezierPoint(tTest);
        Handles.PositionHandle(testOrientedPoint.pos, testOrientedPoint.rot);

        float radius = 0.15f;

        void DrawPoint(Vector2 localPos) => Gizmos.DrawSphere(testOrientedPoint.LocalToWorldPos(localPos), radius);

        for (int i = 0; i < shape2D.vertices.Length; i++)
        {
            DrawPoint(shape2D.vertices[i].point);
        }



    }



    OrientedPoint GetBezierPoint(float t)
    {

        int seg_start = Mathf.FloorToInt(t * (n-1));
        Vector3 first_a = points[seg_start].getAnchorPoint();
        Vector3 first_c = points[seg_start].getSecondControlpoint();
        Vector3 second_c;
        Vector3 second_a;

        if (seg_start + 1 >= points.Length) {
            second_c = points[0].getFirstControlPoint();
            second_a = points[0].getAnchorPoint();
        }
        else
        {
            second_c = points[seg_start + 1].getFirstControlPoint();
            second_a = points[seg_start + 1].getAnchorPoint();
        }
        

        float TActual = (t - seg_start / (float)(n - 1)) / (1.0f / (float)(n-1));
        t = TActual;

        Vector3 a = Vector3.Lerp(first_a, first_c, t);
        Vector3 b = Vector3.Lerp(first_c, second_c, t);
        Vector3 c = Vector3.Lerp(second_c, second_a, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        Vector3 pos = Vector3.Lerp(d, e, t);
        Vector3 tangent = (e - d).normalized;

        return new OrientedPoint(pos, tangent);
    }
}
