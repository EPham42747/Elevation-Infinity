using UnityEngine;

public class LevelColor : MonoBehaviour {
    [Header("To-Edit Properties")]
    [SerializeField] private Material front;
    [SerializeField] private Material back;
    [SerializeField] private new Camera camera;

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

    public void SelectColor() {
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
    }
}