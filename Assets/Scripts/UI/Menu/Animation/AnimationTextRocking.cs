using System.Collections;
using UnityEngine;

public class AnimationTextRocking : MonoBehaviour {
    public float rockSpeed = 2.0f; // Speed of the rocking motion
    public float rockAngle = 10.0f; // Maximum angle of the rocking motion

    private RectTransform rectTransform;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(RockText());
    }

    private IEnumerator RockText() {
        while (true) {
            // Rotate to the right
            yield return RotateToAngle(rockAngle);

            // Rotate back to the left
            yield return RotateToAngle(-rockAngle);
        }
    }

    private IEnumerator RotateToAngle(float targetAngle) {
        var startAngle = rectTransform.rotation.eulerAngles.z;
        var endAngle = targetAngle;

        // Handle angle wrapping
        if (startAngle > 180) startAngle -= 360;
        if (endAngle > 180) endAngle -= 360;

        float t = 0;
        while (t < 1.0f) {
            t += Time.deltaTime * rockSpeed;
            var zAngle = Mathf.Lerp(startAngle, endAngle, t);
            rectTransform.rotation = Quaternion.Euler(0, 0, zAngle);
            yield return null;
        }
    }
}