using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rigidbody2d;
    private PlayerInput input;

    [Header("Rotation")]
    [SerializeField] private GameState gameState;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rotationForce;
    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private float threshold;

    [Header("Velocity Clamping")]
    [SerializeField] private float maxXSpeed;
    [SerializeField] private float maxYSpeed;

    private void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        Boost();
    }

    private void Update() {
        if (gameState.GetState() == GameState.State.Game) {
            Rotate();
        }
        ClampVelocity();
        ClampTorque();
    }

    private void Rotate() {
        if (GetGrounded()) return;
        rigidbody2d.AddTorque(-1 * input.GetXAxis() * rotationForce);
    }

    private void ClampVelocity() {
        rigidbody2d.velocity = new Vector3(Mathf.Clamp(rigidbody2d.velocity.x, 0f, maxXSpeed), Mathf.Clamp(rigidbody2d.velocity.y, -1 * maxYSpeed, 0f), 0f);
    }

    private void ClampTorque() {
        rigidbody2d.angularVelocity = Mathf.Clamp(rigidbody2d.angularVelocity, -maxRotationSpeed, maxRotationSpeed);
    }

    // For getting the player moving at the beginning
    public void Boost() {
        rigidbody2d.AddForce(Vector2.right * 5f, ForceMode2D.Impulse);
    }

    public bool GetGrounded() {
        return Physics2D.Raycast(groundChecker.position, Vector2.down, checkDistance, groundLayer);
    }

    public float GetNormalizedVelocity() {
        float current =  Mathf.Sqrt(Mathf.Pow(rigidbody2d.velocity.x, 2f) + Mathf.Pow(rigidbody2d.velocity.y, 2f));
        float max = Mathf.Sqrt(Mathf.Pow(maxXSpeed, 2f) + Mathf.Pow(maxYSpeed, 2f));
        return current / max;
    }

    public void ResetTorque() {
     rigidbody2d.angularVelocity = 0f;
    }
 }