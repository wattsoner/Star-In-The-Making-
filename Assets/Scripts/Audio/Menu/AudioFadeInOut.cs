using System.Collections;
using UnityEngine;

public class AudioFadeInOut : MonoBehaviour {
    public float minVol, maxVol;
    public float fadeDuration = 2.0f;

    public AudioSource audioSource;

    private Coroutine currentCoroutine;

    private void Start() {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        FadeIn();
    }

    private void FadeIn() {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(FadeInCoroutine());
    }

    private void FadeOut() {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeInCoroutine() {
        var startVolume = minVol;
        audioSource.volume = startVolume;
        audioSource.Play();

        while (audioSource.volume < maxVol) {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = maxVol;
    }

    private IEnumerator FadeOutCoroutine() {
        var startVolume = audioSource.volume;

        while (audioSource.volume > 0.0f) {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = 0.0f;
        audioSource.Stop();
    }
}