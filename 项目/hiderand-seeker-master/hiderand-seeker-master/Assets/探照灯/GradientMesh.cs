using UnityEngine;
using System.Collections.Generic;

public class GradientMesh : MonoBehaviour
{
    public Material gradientMaterial;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    Mesh mesh;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        mesh = new Mesh();
        meshFilter.mesh = mesh;

        GenerateMesh();
    }

    void GenerateMesh()
    {
        Vector3[] vertices = new Vector3[4];
        int[] triangles = new int[6];
        Color[] colors = new Color[4];

        // Define vertices
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(1, 0, 0);
        vertices[2] = new Vector3(0, 1, 0);
        vertices[3] = new Vector3(1, 1, 0);

        // Define triangles
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 1;

        // Define colors (gradient)
        colors[0] = Color.red;
        colors[1] = Color.yellow;
        colors[2] = Color.green;
        colors[3] = Color.blue;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();

        meshRenderer.material = gradientMaterial;
    }
}
