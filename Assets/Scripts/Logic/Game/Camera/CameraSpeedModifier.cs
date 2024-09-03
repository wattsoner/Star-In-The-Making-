using UnityEngine;

public class CameraSpeedModifier : MonoBehaviour {
    public float followSpeed = 0.5f;
    public float multiplier = 1f;

    private readonly float multiplierIncreaseRate = 0.05f;
    private readonly float speedIncreaseRate = 0.0075f;

    private void Update() {
        followSpeed += speedIncreaseRate * Time.deltaTime;
        multiplier += multiplierIncreaseRate * Time.deltaTime;
    }

    public float GetFollowSpeed() {
        return followSpeed;
    }

    public float GetMultiplier() {
        return multiplier;
    }
}