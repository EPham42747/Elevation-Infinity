using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour {
    private Camera cam;

    [Header("Follow")]
    [SerializeField] private Transform target;
    [SerializeField] private float followLerpTime;

    [Header("Zoom")]
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float zoomLerpTime;
    private void Start() {
        cam = GetComponent<Camera>();
    }

    private void Update() {
        Follow();
        Zoom();
    }

    private void Follow() {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), followLerpTime);
    }

    private void Zoom() {
        cam.fieldOfView = Mathf.Clamp(Mathf.Lerp(cam.fieldOfView, movement.GetNormalizedVelocity() * maxZoom, zoomLerpTime), minZoom, maxZoom);
    }
}