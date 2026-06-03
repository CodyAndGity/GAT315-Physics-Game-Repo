using System.Collections.Generic;
using UnityEngine;

public class VoxelWorld : MonoBehaviour
{
    [SerializeField] GameObject voxelPrefab;
    [SerializeField, Range(1, 200)] int numX;
    [SerializeField, Range(1, 200)] int numZ;

    [SerializeField, Range(-10.0f, 10.0f)] float height =1;
    [SerializeField, Range(-0.2f, 0.2f)] float perlinScale = 0.05f;
    [SerializeField, Range(-5.0f, 5.0f)] float perlinRateX = 1.0f;
    [SerializeField, Range(-5.0f, 5.0f)] float perlinRateZ = 1.0f;

    [SerializeField, Range(0.0f, 1.0f)] float threshold = 0.0f;
    [SerializeField] Gradient gradient;
    [SerializeField] bool quantize = false;
    [SerializeField] bool sharp = false;

    List<GameObject> voxels = new();
    bool reset = true;

    float perlinOffsetX = 0.0f;
    float perlinOffsetZ = 0.0f;


    private void OnValidate()
    {
        reset = true;
    }

    void Update()
    {
        if (reset)
        {
            CreateVoxels();
            reset = false;
        }
        perlinOffsetX += perlinRateX * Time.deltaTime;
        perlinOffsetZ += perlinRateZ * Time.deltaTime;

        UpdateVoxels();
    }

    void CreateVoxels()
    {
        // destroy and remove all voxels
        foreach (var voxel in voxels) { Destroy(voxel.gameObject); }
        voxels.Clear();

        for (int i = 0; i < numX * numZ; i++)
        {
            var voxel = Instantiate(voxelPrefab, transform);
            voxels.Add(voxel);
        }
    }

    void UpdateVoxels()
    {
        // update height
        for (int i = 0; i < voxels.Count; i++)
        {
            float x = (i % numX);
            float z = (i / numX);
            float noise = SampleNoise(x, z);
            float y = noise * height;
            if (quantize) y = Mathf.Floor(y);

            bool active = noise > threshold;
            voxels[i].SetActive(active);

            Vector3 position = Vector3.zero;
            position.x = x - (numX / 2);
            position.y = y;
            position.z = z - (numZ / 2);
            voxels[i].transform.position = position;
            ApplyColor(voxels[i], noise);
        }
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

    void ApplyColor(GameObject voxel, float t)
    {
        if (!voxel.TryGetComponent<Renderer>(out var renderer)) return;

        // MaterialPropertyBlock avoids creating a unique material instance per voxel
        var block = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(block);
        block.SetColor("_Color", gradient.Evaluate(t));
        block.SetColor("_BaseColor", gradient.Evaluate(t));  // URP shader property
        renderer.SetPropertyBlock(block);
    }
}
