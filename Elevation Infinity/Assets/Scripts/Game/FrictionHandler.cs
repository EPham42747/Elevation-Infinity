using UnityEngine;

public class FrictionHandler : MonoBehaviour {
    [SerializeField] private GameState gameState;
    [SerializeField] private PhysicsMaterial2D physicsMaterial;
    [SerializeField] private float frictionValue;

    private void Update() {
        if (gameState.GetState() == GameState.State.Death)  physicsMaterial.friction = frictionValue;
        else physicsMaterial.friction = 0f;
    }
}