using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelColor : MonoBehaviour {
    [SerializeField] private Material material;
    [SerializeField] private Gradient gradient;
    private float seed;
    private Color color;
    
    private void Start() {
        seed = Random.Range(0f, 1f);
        color = gradient.Evaluate(seed);
    }

    private void SetColors() {
        material.color = color;
    }
}