using System.Collections;
using UnityEngine;

public class CameraDelay : MonoBehaviour {
    [SerializeField] private LineRenderer levelLine;
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private Transform player;
    [SerializeField] private float xOffset;
    private bool triggered = false;

    private void Start() {
        StartCoroutine(SetPosition());
        cameraMovement.enabled = false;
    }

    private void Update() {
        Delay();
    }

    private void Delay() {
        if (transform.position.x < player.position.x + 2f && triggered == false) {
            cameraMovement.enabled = true;
            triggered = true;
        }
    }

    private IEnumerator SetPosition() {
        yield return new WaitForSecondsRealtime(0.1f);
        
        int i = 0;
        while (xOffset > levelLine.GetPosition(i).x) i++;

        transform.position = new Vector3(levelLine.GetPosition(i).x, levelLine.GetPosition(i).y + 0.5f, -10f);
        yield return null;
    }

    public void Reset() {
        StartCoroutine(SetPosition());
        cameraMovement.enabled = false;
        triggered = false;
    }
}