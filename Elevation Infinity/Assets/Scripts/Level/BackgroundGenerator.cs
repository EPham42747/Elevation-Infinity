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

    [Header("Background Generator")]
    [SerializeField] private float yOffset;
    [SerializeField] private int numPositions;
    [SerializeField] private float maxIncrement;
    [SerializeField] private float size;
    [SerializeField] private float yScale;
    [SerializeField] private float zPosition;

    [Header("Resource Management")]
    [SerializeField] private Transform player;
    [SerializeField] private float despawnThreshold;

    private float x = 0f;
    private int a = 0;
    private float seed;
    private Vector3[] positions;

    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();

        seed = Random.Range(0f, 1f);

        CreateLine();
        UpdateFill();
    }

    private void Update() {
        while (PastThreshold()) {
            UpdateLine();
            UpdateFill();
        }
    }

    private void CreateLine() {
        positions = new Vector3[numPositions];
        lineRenderer.positionCount = numPositions;

        positions[0] = Vector3.left * 20f;

        for (int i = 1; i < numPositions; i++) {
            float perlin = Mathf.PerlinNoise(i / size, seed);
            float increment = (1 - perlin) * maxIncrement;

            positions[i] = new Vector3(x, positions[i - 1].y - yScale * perlin, zPosition);
            x += increment;
        }
        for (int i = 0; i < numPositions; i++) positions[i].y += yOffset;

        a = numPositions;

        lineRenderer.SetPositions(positions);
    }

    private void UpdateLine() {
        // Shift all positions to the left
        for (int i = 1; i < numPositions; i++) {
            positions[i - 1] = positions[i];
        }

        // Create new position
        float perlin = Mathf.PerlinNoise(a / size, seed);
        float increment = (1 - perlin) * maxIncrement;

        positions[numPositions - 1] = new Vector3(x, positions[numPositions - 2].y - yScale * perlin, zPosition);
        x += increment;
        a++;

        lineRenderer.SetPositions(positions);
    }

    private void UpdateFill() {
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

    private bool PastThreshold() {
        if (positions[0].x < player.position.x - despawnThreshold) return true;
        return false;
    }
}