using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimationSpriteSheetButtons : MonoBehaviour, IPointerEnterHandler {
    public Image buttonImage; // Reference to the Image component of the button
    public Sprite[] sprites; // Array of sprites (frames) from the spritesheet
    public float frameRate = 0.1f; // Time between frames
    private int currentFrame;
    private bool isAnimating;

    private void Start() {
        if (buttonImage == null) buttonImage = GetComponent<Image>(); // Get the Image component if not assigned
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!isAnimating) StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation() {
        isAnimating = true;
        currentFrame = 0;

        while (currentFrame < sprites.Length) {
            buttonImage.sprite = sprites[currentFrame];
            currentFrame++;
            yield return new WaitForSeconds(frameRate);
        }

        isAnimating = false;
    }
}