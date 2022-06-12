using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour {
    [SerializeField] private GameState gameState;

    private void Update() {
        if (gameState.GetState() == GameState.State.Main) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }
}