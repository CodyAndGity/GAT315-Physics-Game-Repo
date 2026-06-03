using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static void Plane(MeshFilter meshFilter, float xSize, float zSize, int xVertexNum, int zVertexNum)
    {
        // get the mesh
        Mesh mesh = meshFilter.mesh;
        mesh.Clear();

        // create vertices (position)
        Vector3[] vertices = new Vector3[xVertexNum * zVertexNum];
        for (int z = 0; z < zVertexNum; z++)
        {
            // [ -length / 2, length / 2 ]
            float zPosition = ((float)z / (zVertexNum - 1) - 0.5f) * xSize;
            for (int x = 0; x < xVertexNum; x++)
            {
                // [ -width / 2, width / 2 ]
                float xPosition = ((float)x / (xVertexNum - 1) - 0.5f) * zSize;

                vertices[x + z * xVertexNum] = new Vector3(xPosition, 0f, zPosition);
            }
        }

        // create normals
        Vector3[] normals = new Vector3[vertices.Length];
        Array.ForEach(normals, normal => normal = Vector3.up) ;

        // create tangents
        Vector4[] tangents = new Vector4[vertices.Length];
        Array.ForEach(tangents, normal => normal = new Vector4(1, 0, 0, -1));

        // create uvs (0-1)
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int v = 0; v < zVertexNum; v++)
        {
            for (int u = 0; u < xVertexNum; u++)
            {
                uvs[u + v * xVertexNum] = new Vector2((float)u / (xVertexNum - 1), (float)v / (zVertexNum - 1));
            }
        }
        //create colors
        Color[] colors = new Color[vertices.Length];
        Array.ForEach(colors, color => color = new Color(1,0,0,0));

        // create triangles
        int numFaces = (xVertexNum - 1) * (zVertexNum - 1);
        int[] triangles = new int[numFaces * 6];
        int t = 0;
        for (int face = 0; face < numFaces; face++)
        {
            // Retrieve lower left corner from face index
            int i = face % (xVertexNum - 1) + (face / (zVertexNum - 1) * xVertexNum);

            triangles[t++] = i + xVertexNum;
            triangles[t++] = i + 1;
            triangles[t++] = i;

            triangles[t++] = i + xVertexNum;
            triangles[t++] = i + xVertexNum + 1;
            triangles[t++] = i + 1;
        }

        // set mesh data
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.tangents = tangents;
        mesh.uv = uvs;
        mesh.colors = colors;
        mesh.triangles = triangles;

        // recalculate mesh values
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }
}
