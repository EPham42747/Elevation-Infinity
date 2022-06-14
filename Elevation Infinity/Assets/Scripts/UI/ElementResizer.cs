using UnityEngine;

public class ElementResizer : MonoBehaviour {
    [SerializeField] private RectTransform menuUI;
    [SerializeField] private RectTransform gameUI;
    [SerializeField] private RectTransform deathUI;
    [SerializeField] private float scale;

    private void Start() {
        menuUI.localScale = Vector3.one * Screen.width / scale;
        gameUI.localScale = Vector3.one * Screen.width / scale;
        deathUI.localScale = Vector3.one * Screen.width / scale;
    }
}