using UnityEngine;

// This class isn't really needed but helps for organization if I decide to keep working on the game
public class PlayerInput : MonoBehaviour {
    public float GetXAxis() {
        return Input.GetAxis("Horizontal");
    }

    public bool GetClick() {
        return Input.GetMouseButtonDown(0);
    }
}