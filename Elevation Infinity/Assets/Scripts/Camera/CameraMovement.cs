using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour {
    private Camera cam;

    [Header("Follow")]
    [SerializeField] private Transform target;
    [SerializeField] private float followTime;

    [Header("Zoom")]
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float zoomTime;

    private void Start() {
        cam = GetComponent<Camera>();
    }

    private void Update() {
        Follow();
        Zoom();
    }

    private void Follow() {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, -10f), followTime * Time.deltaTime);
    }

    private void Zoom() {
        cam.orthographicSize = Mathf.Clamp(Mathf.Lerp(cam.orthographicSize, movement.GetNormalizedVelocity() * maxZoom, zoomTime * Time.deltaTime), minZoom, maxZoom);
    }
}