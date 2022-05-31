using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour {
    #region Properties
    private Rigidbody2D rigidbody2d;
    private PlayerInput input;

    [Header("Rotation")]
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rotationSpeed;

    [Header("Velocity Clamping")]
    [SerializeField] private float maxXSpeed;
    [SerializeField] private float maxYSpeed;
    #endregion

    private void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
    }

    private void Update() {
        Rotate();
        ClampVelocity();
        ResetAngularVelocity();
    }

    private void Rotate() {
        if (GetGrounded()) return;
        transform.Rotate(-1 * Vector3.forward * input.GetXAxis() * rotationSpeed * Time.deltaTime);
    }

    private void ClampVelocity() {
        rigidbody2d.velocity = new Vector3(Mathf.Clamp(rigidbody2d.velocity.x, 0f, maxXSpeed), Mathf.Clamp(rigidbody2d.velocity.y, -1 * maxYSpeed, 0f), 0f);
    }

    private void ResetAngularVelocity() {
        if (!GetGrounded()) rigidbody2d.angularVelocity = 0f;
    }

    public bool GetGrounded() {
        return Physics2D.Raycast(groundChecker.position, Vector2.down, checkDistance, groundLayer);
    }

    public float GetNormalizedVelocity() {
        float current =  Mathf.Sqrt(Mathf.Pow(rigidbody2d.velocity.x, 2f) + Mathf.Pow(rigidbody2d.velocity.y, 2f));
        float max = Mathf.Sqrt(Mathf.Pow(maxXSpeed, 2f) + Mathf.Pow(maxYSpeed, 2f));
        return current / max;
    }
}