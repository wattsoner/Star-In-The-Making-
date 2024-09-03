using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [Header("Target to Follow")] [SerializeField]
    private Transform target; // The sprite that the camera will follow

    [Header("Camera Settings")] [SerializeField]
    private Vector3 offset = new(0f, 0f, -10f); // Offset to maintain distance from the sprite

    [SerializeField] private float smoothSpeed = 0.125f; // Speed at which the camera will catch up to the target

    private void LateUpdate() {
        if (target == null) return;

        // Desired position is the target's position plus the offset (only x and y)
        var desiredPosition = new Vector3(target.position.x, target.position.y, offset.z);

        // Smoothly move the camera towards the desired position
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}