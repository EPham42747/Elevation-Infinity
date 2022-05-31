using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SnowEffect : MonoBehaviour {
    private ParticleSystem particles;

    [Header("Emission")]
    [SerializeField] private float emissionRate;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask groundLayer;
    private ParticleSystem.EmissionModule emission;

    [Header("Movement")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform reference;

    [Header("Rotation")]
    [SerializeField] private Transform player;
    [SerializeField] private float rotationOffset;

    private void Start() {
        particles = GetComponent<ParticleSystem>();
        emission = particles.emission;
    }

    private void Update() {
        SetEnable();
        Move();
        Rotate();
    }

    private void SetEnable() {
        // Triggers when tip of sled touches ground
        if (GetGrounded()) emission.rateOverTimeMultiplier = emissionRate;
        else emission.rateOverTimeMultiplier = 0f;
    }

    private void Move() {
        transform.position = reference.position;
    }

    private void Rotate() {
        // Triggers when all of sled touches ground
        if (playerMovement.GetGrounded()) transform.eulerAngles = player.eulerAngles + Vector3.forward * rotationOffset;
    }

    private bool GetGrounded() { return Physics2D.Raycast(groundChecker.position, Vector2.down, checkDistance, groundLayer); }
}