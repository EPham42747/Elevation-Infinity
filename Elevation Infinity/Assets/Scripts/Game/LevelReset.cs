using UnityEngine;

public class LevelReset : MonoBehaviour {
    [SerializeField] private GameState gameState;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private BackgroundGenerator backgroundGenerator;
    [SerializeField] private LevelColor levelColor;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 respawnPosition;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private new CameraDelay camera;

    private void Update() {
        if (playerInput.GetClick() && gameState.GetState() == GameState.State.Main) {
            levelGenerator.Reset();
            backgroundGenerator.Reset();
            levelColor.SelectColor();

            player.position = respawnPosition;
            player.rotation = Quaternion.Euler(Vector3.zero);
            playerMovement.ResetTorque();

            camera.Reset();
        }
        else if (playerInput.GetClick() && gameState.GetState() == GameState.State.Main) {
            playerMovement.Boost();
        }
    }
}