using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerDeath : MonoBehaviour {
    [Header("Ground Check")]
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask groundLayer;

    [Header("Execute On Death")]
    [SerializeField] private GameState gameState;
    [SerializeField] private ParticleSystem snowEffect;
    private PlayerInput playerInput;

    private void Start() {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update() {
        if (GetGrounded() && gameState.GetState() == GameState.State.Game) Die();
    }

    private void Die() {
        gameState.DeathGameState();
        playerInput.enabled = false;
    }

    private bool GetGrounded() {
        return Physics2D.Raycast(transform.position, transform.up, checkDistance, groundLayer);
    }
}