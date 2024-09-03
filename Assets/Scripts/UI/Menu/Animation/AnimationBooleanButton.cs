using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimationBooleanButton : MonoBehaviour {
    public Image buttonImage; // Reference to the Image component of the button
    public Sprite[] sprites; // Array of sprites (frames) from the spritesheet
    public float frameRate = 0.1f; // Time between frames
    private int currentFrame;
    private bool isAnimating;
    private bool playForward = true; // Tracks whether to play the animation forward or backward

    private void Start() {
        if (buttonImage == null) buttonImage = GetComponent<Image>(); // Get the Image component if not assigned
    }

    public void OnButtonClick() {
        if (!isAnimating) StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation() {
        isAnimating = true;

        if (playForward)
            for (currentFrame = 0; currentFrame < sprites.Length; currentFrame++) {
                buttonImage.sprite = sprites[currentFrame];
                yield return new WaitForSeconds(frameRate);
            }
        else
            for (currentFrame = sprites.Length - 1; currentFrame >= 0; currentFrame--) {
                buttonImage.sprite = sprites[currentFrame];
                yield return new WaitForSeconds(frameRate);
            }

        playForward = !playForward; // Toggle the direction for the next click
        isAnimating = false;
    }
}