using UnityEngine;
using System.Collections;

public class FlashOnHit : MonoBehaviour {
    [SerializeField] private float flashDuration = 0.15f;

    private Renderer[] renderers;
    private Color[] originalColors;
    private Coroutine flashCoroutine;

    private void Awake() {
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++) {
            originalColors[i] = renderers[i].material.color;
        }
    }

    public void Flash() {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine() {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = Color.red;

        yield return new WaitForSeconds(flashDuration);

        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = originalColors[i];
    }
}