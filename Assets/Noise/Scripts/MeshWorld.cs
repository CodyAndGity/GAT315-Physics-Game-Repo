using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshCollider)), RequireComponent(typeof(MeshRenderer))]
public class MeshWorld : MonoBehaviour
{
    [Header("Mesh Generator")]
    [SerializeField][Range(1, 80)] float sizeX = 40;
    [SerializeField][Range(1, 80)] float sizeZ = 40;
    [SerializeField][Range(2, 80)] int numX = 80;
    [SerializeField][Range(2, 80)] int numZ = 80;

    MeshFilter meshFilter;
    MeshCollider meshCollider;

    Mesh mesh;
    Vector3[] vertices;
    Color[] colors;
    Vector3[,] buffer;

    [Header("Noise")]
    [SerializeField, Range(-10.0f, 10.0f)] float height = 1;

    [SerializeField, Range(-0.2f, 0.2f)] float perlinScale = 0.05f;
    [SerializeField, Range(-5.0f, 5.0f)] float perlinRateX = 1.0f;
    [SerializeField, Range(-5.0f, 5.0f)] float perlinRateZ = 1.0f;
    [SerializeField] Gradient gradient;

    [SerializeField] bool quantize = false;
    [SerializeField] bool sharp = false;
    bool reset = true;

    float perlinOffsetX = 0.0f;
    float perlinOffsetZ = 0.0f;


    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        MeshGenerator.Plane(meshFilter, sizeX, sizeZ, numX, numZ);

        mesh = meshFilter.mesh;
        vertices = mesh.vertices;
        colors = mesh.colors;

        buffer = new Vector3[numX, numZ];
    }
    private void OnValidate()
    {
        reset = true;
    }

    void Update()
    {
        if (reset)
        {
            meshFilter = GetComponent<MeshFilter>();
            meshCollider = GetComponent<MeshCollider>();

            MeshGenerator.Plane(meshFilter, sizeX, sizeZ, numX, numZ);

            mesh = meshFilter.mesh;
            vertices = mesh.vertices;
            colors = mesh.colors;


            buffer = new Vector3[numX, numZ];
        }
        perlinOffsetX += perlinRateX * Time.deltaTime;
        perlinOffsetZ += perlinRateZ * Time.deltaTime;
        UpdateWorld();
        UpdateMesh();
    }

    void UpdateWorld()
    {
        // update vertex values
        for (int i = 0; i < vertices.Length; i++)
        {
            float x = (i % numX);
            float z = (i / numX);




            float noise = SampleNoise(x, z);
            float y = noise * height;
            if (quantize) y = Mathf.Floor(y);

            Vector3 position = Vector3.zero;
            position.x = ((x / (float)(numX - 1)) - 0.5f) * sizeX;
            position.z = ((z / (float)(numZ - 1)) - 0.5f) * sizeZ;
            position.y = y;

            vertices[i] = position;
            colors[i] = gradient.Evaluate(noise);

        }
    }

    void UpdateMesh()
    {


        // recalculate mesh with new vertex values
        mesh.vertices = vertices;
        mesh.colors = colors;

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.RecalculateBounds();
        meshCollider.sharedMesh = mesh;
    }

    float SampleNoise(float x, float z)
    {
        float xs = (x + perlinOffsetX) * perlinScale;
        float zs = (z + perlinOffsetZ) * perlinScale;

        float value = Mathf.PerlinNoise(xs, zs);

        if (sharp)
        {
            // classic ridged: invert the absolute value to make sharp peaks
            value = 1.0f - Mathf.Abs(value * 2.0f - 1.0f);
        }
        else
        {
            // puffy rounded hills
            value = Mathf.Abs(value * 2.0f - 1.0f);
        }

        return value;
    }
}
