using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class BackgroundGenerator : MonoBehaviour {
    private LineRenderer lineRenderer;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private float yOffset;
    private int numPositions;
    private float maxIncrement;
    private float size;
    private float yScale;
    private float x = 0;
    private float seed;

    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();

        numPositions = levelGenerator.GetNumPositions();
        maxIncrement = levelGenerator.GetMaxIncrement();
        size = levelGenerator.GetSize();
        yScale = levelGenerator.GetYScale();

        seed = Random.Range(0f, 1f);

        CreateLine();
        CreateFill();
    }

    private void Update() {
        
    }

    private void CreateLine() {
        Vector3[] positions = new Vector3[numPositions];
        lineRenderer.positionCount = numPositions;

        positions[0] = Vector3.left * 20f;

        for (int i = 1; i < numPositions; i++) {
            float increment = 0f;
            float perlin = Mathf.PerlinNoise(i / size, seed);
            increment = (1 - perlin) * maxIncrement;

            positions[i] = new Vector3(x, positions[i - 1].y - yScale * perlin, -1f);
            x += increment;
        }
        for (int i = 0; i < numPositions; i++) positions[i].y += yOffset;

        lineRenderer.SetPositions(positions);
    }

    private void CreateFill() {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // For every position in LineRenderer, add that point and a second one super far down
        for (int i = 0, j = 0; i < numPositions * 2; i += 2, j++) {
            Vector3 linePosition = lineRenderer.GetPosition(j);
            vertices.Add(linePosition);
            vertices.Add(new Vector3(linePosition.x, -1000f, 0f));
        }

        // Connect every group of vertices into triangles
        for (int i = 0; i < (numPositions - 1) * 2; i += 2) {
            triangles.Add(i);
            triangles.Add(i + 1);
            triangles.Add(i + 2);
            triangles.Add(i + 2);
            triangles.Add(i + 1);
            triangles.Add(i + 3);
        }

        Mesh fill = new Mesh();
        fill.SetVertices(vertices);
        fill.SetTriangles(triangles, 0);
        meshFilter.mesh = fill;
    }
}