using UnityEngine;
using UnityEngine.UI;

public class AnimationBoostFade : MonoBehaviour {
    
    [Header("Boost Images")]
    [SerializeField] private RawImage[] boostImages;
    
    private float fadeDuration = 0.25f;
    private float fadeProgress = 0f;

    private readonly Color activeColor = Color.white;
    private readonly Color inactiveColor = Color.grey;
    

    private void Awake() {
        if (boostImages.Length != 3) {
            return;
        }

        foreach (var t in boostImages) t.color = activeColor;
    }

    public void UpdateBoostUI(int availableBoosts) {
        fadeProgress = Mathf.Clamp01(Time.deltaTime / fadeDuration);

        for (var i = 0; i < boostImages.Length; i++) {
            var targetColor = i < availableBoosts ? activeColor : inactiveColor;
            boostImages[i].color = Color.Lerp(boostImages[i].color, targetColor, fadeProgress);
        }
    }
}