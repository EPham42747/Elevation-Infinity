using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour {
    [SerializeField] private GameState gameState;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject deathUI;

    private void Update() {
        if (gameState.GetState() == GameState.State.Main) {
            menuUI.SetActive(true);
            gameUI.SetActive(false);
            deathUI.SetActive(false);
        }
        else if (gameState.GetState() == GameState.State.Game) {
            menuUI.SetActive(false);
            gameUI.SetActive(true);
            deathUI.SetActive(false);
        }
        else {
            menuUI.SetActive(false);
            gameUI.SetActive(false);
            deathUI.SetActive(true);
        }
    }
}