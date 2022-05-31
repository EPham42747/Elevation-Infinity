using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(EdgeCollider2D))]
public class LevelGenerator : MonoBehaviour {
    private LineRenderer lineRenderer;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private EdgeCollider2D edgeCollider;
    [SerializeField] private int numPositions;
    [SerializeField] private float size;
    [SerializeField] private float maxIncrement;
    [SerializeField] private float yScale;
    private float x = 0;
    private float seed;

    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        seed = Random.Range(0f, 1f);

        CreateLine();
        CreateFill();
        CreateCollider();
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

            positions[i] = new Vector3(x, positions[i - 1].y - yScale * perlin, 0f);
            x += increment;
        }

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

    private void CreateCollider() {
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < lineRenderer.positionCount; i++) {
            points.Add((Vector2)(lineRenderer.GetPosition(i)));
        }
        edgeCollider.SetPoints(points);
    }

    public int GetNumPositions() { return numPositions; }
    public float GetSize() { return size; }
    public float GetMaxIncrement() { return maxIncrement; }
    public float GetYScale() { return yScale; }
}