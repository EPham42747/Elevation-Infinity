using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTrail : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 offset;

    private void Update() {
        Move();
    }

    private void Move() {
        transform.position = target.position + new Vector3(offset.x, offset.y, 0f);
    }
}