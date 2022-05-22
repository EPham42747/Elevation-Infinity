using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class LevelGenerator : MonoBehaviour {
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    [SerializeField] private int numPositions;
    [SerializeField] private float scale;
    private int x = 0;

    private void Start() {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        edgeCollider = gameObject.GetComponent<EdgeCollider2D>();
        CreateLine();
    }
    private void Update() {

    }

    private void CreateLine() {
        Vector3[] positions = new Vector3[numPositions];
        lineRenderer.positionCount = numPositions;
        for (int index = 0; index < numPositions; index++) {
            if (index != 0) positions[index] = new Vector3(x, positions[index - 1].y - Mathf.PerlinNoise(scale, 0f), 0f);
            x++;
        }
        lineRenderer.SetPositions(positions);
    }
}