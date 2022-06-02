using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelColor : MonoBehaviour {
    [Header("To-Edit Properties")]
    [SerializeField] private Material front;
    [SerializeField] private Material back;
    [SerializeField] private new Camera camera;
    [SerializeField] private ParticleSystem particles;

    [Header("Color Schemes")]
    [SerializeField] private Gradient orange;
    [SerializeField] private Gradient red;
    [SerializeField] private Gradient pink;
    [SerializeField] private Gradient purple;
    [SerializeField] private Gradient blue;

    private int seed;
    private Color color;
    
    private void Start() {
        SelectColor();
    }

    private void SelectColor() {
        seed = (int)(Random.Range(0f, 5f));
        
        switch (seed) {
            case 0:
                EditMaterials(orange);
                break;
            case 1:
                EditMaterials(red);
                break;
            case 2:
                EditMaterials(pink);
                break;
            case 3:
                EditMaterials(purple);
                break;
            case 4:
                EditMaterials(blue);
                break;
            default:
                EditMaterials(orange);
                break;
        }
    }

    private void EditMaterials(Gradient gradient) {
        front.color = gradient.Evaluate(0f);
        back.color = gradient.Evaluate(0.5f);

        camera.backgroundColor = gradient.Evaluate(1f);
        
        
        Gradient minimum = new Gradient(), maximum = new Gradient();
        GradientAlphaKey[] minAlpha = new GradientAlphaKey[2], maxAlpha = new GradientAlphaKey[2];
        GradientColorKey[] color = new GradientColorKey[2];

        minAlpha[0] = new GradientAlphaKey(1f / 255f * 125f, 0f);
        minAlpha[1] = new GradientAlphaKey(0f, 0.75f);
        maxAlpha[0] = new GradientAlphaKey(1f / 255f * 200f, 0f);
        maxAlpha[1] = new GradientAlphaKey(0f, 1f);
        color[0] = new GradientColorKey(gradient.Evaluate(0f), 0f);
        color[1] = new GradientColorKey(gradient.Evaluate(0f), 1f);

        minimum.SetKeys(color, minAlpha);
        maximum.SetKeys(color, maxAlpha);

        ParticleSystem.MinMaxGradient minMax = new ParticleSystem.MinMaxGradient(minimum, maximum);

        var main = particles.main;
        var mode = particles.main.startColor.mode;
        mode = ParticleSystemGradientMode.TwoGradients;
        main.startColor = minMax;
    }
}