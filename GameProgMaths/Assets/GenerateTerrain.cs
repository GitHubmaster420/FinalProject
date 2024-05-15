using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    [System.Serializable]
    public class NoiseParams
    {
        public float FrequencyScale;
        public float AmplitudeScale;

        NoiseParams()
        {
            FrequencyScale = 1.0f;
            AmplitudeScale = 1.0f;
        }
    }

    [Range(1.0f, 1000f)]
    public float Size = 100f;
    [Range(2, 255)]
    public int Segments = 100;

    [SerializeField]
    public NoiseParams[] NoiseLayers;

    public float AmplitudeScaler = 10f;
    [Range (1, 100)]
    public float FrequencyScaler = 1.0f;

    private Mesh MyMesh = null;



    private void Awake()
    {
        GenerateMesh();
    }

    private void OnValidate()
    {
        GenerateMesh();
    }

    public void GenerateMesh()
    {
        if (MyMesh == null)
            MyMesh = new Mesh();
        else
            MyMesh.Clear();

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int y_seg = 0; y_seg <= Segments; y_seg++)
        {
            for (int x_seg = 0; x_seg <= Segments; x_seg++)
            {
                float x = x_seg * (Size / (float)Segments);
                float y = y_seg * (Size / (float)Segments);
                float z = 0;
                for(int i = 0; i < NoiseLayers.Length; i++)
                {
                    z += (Mathf.PerlinNoise(x / NoiseLayers[i].FrequencyScale, y / NoiseLayers[i].FrequencyScale) - 0.5f)
                        * NoiseLayers[i].AmplitudeScale;
                }
                if (z < 0)
                {
                    z = 0;
                }

                Vector3 vert = new Vector3(x, z, y);
                Vector2 uv = new Vector2(x / Size, y / Size);
                uvs.Add(uv);
                Debug.Log(vert);
                vertices.Add(vert);

            }
        }

        for (int y_seg = 0; y_seg < Segments; y_seg++)
        {
            for (int x_seg = 0; x_seg < Segments; x_seg++)
            {
                int TopLeft = x_seg + y_seg * (Segments + 1);
                int TopRight = TopLeft + 1;
                int BotLeft = TopLeft + Segments + 1;
                int BotRight = BotLeft + 1;

                // 1st tri
                triangles.Add(TopLeft);
                triangles.Add(BotLeft);
                triangles.Add(TopRight);

                //2nd tri
                triangles.Add(TopRight);
                triangles.Add(BotLeft);
                triangles.Add(BotRight);

            }
        }

        MyMesh.SetVertices(vertices);
        MyMesh.SetTriangles(triangles, 0);
        MyMesh.RecalculateNormals();
        MyMesh.SetUVs(0, uvs);
        GetComponent<MeshFilter>().sharedMesh = MyMesh;

            
    }
}
