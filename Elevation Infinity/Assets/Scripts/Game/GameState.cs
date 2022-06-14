using UnityEngine;

public class GameState : MonoBehaviour {
    [SerializeField] private PlayerInput playerInput;
    public enum State { Main, Game, Death }
    private State currentState;

    private void Start() {
        currentState = State.Main;
    }
    
    private void Update() {
        if (playerInput.GetClick()) AdvanceGameState();
    }
    
    // Game cycles: Main -> Game -> Death
    private void AdvanceGameState() {
        if (currentState == State.Main) currentState = State.Game;
        else if (currentState == State.Death) currentState = State.Main;
    }

    public State GetState() {
        return currentState;
    }

    public void DeathGameState() {
        currentState = State.Death;
    }
}