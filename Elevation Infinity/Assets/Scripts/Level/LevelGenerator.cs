using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class LevelGenerator : MonoBehaviour {
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    [SerializeField] private int numPositions;
    [SerializeField] private float size;
    [SerializeField] private float maxIncrement;
    [SerializeField] private float yScale;
    private float x = 0;

    private void Start() {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        edgeCollider = gameObject.GetComponent<EdgeCollider2D>();
        CreateLine();
        CreateCollider();
    }
    private void Update() {

    }

    private void CreateLine() {
        Vector3[] positions = new Vector3[numPositions];
        lineRenderer.positionCount = numPositions;
        for (int index = 0; index < numPositions; index++) {
            float increment = 0f;
            if (index == 0) positions[index] = Vector3.zero;
            else {
                float perlin = Mathf.PerlinNoise(index / size, 0f);
                increment = (1 - perlin) * maxIncrement;
                positions[index] = new Vector3(x, positions[index - 1].y - yScale * perlin, 0f);
            }
            x += increment;
        }
        lineRenderer.SetPositions(positions);
    }

    private void CreateCollider() {
        List<Vector2> points = new List<Vector2>();
        for (int index = 0; index < lineRenderer.positionCount; index++) {
            points.Add((Vector2)(lineRenderer.GetPosition(index)));
        }
        edgeCollider.SetPoints(points);
    }
}