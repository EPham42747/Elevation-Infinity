using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public float GetXAxis() {
        return Input.GetAxis("Horizontal");
    }

    public bool GetClick() {
        return Input.GetMouseButtonDown(0);
    }
}