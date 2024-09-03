using TMPro;
using UnityEngine;

public class PointController : MonoBehaviour {
    [SerializeField] private float updateInterval = 1.0f;

    [SerializeField] private TextMeshProUGUI pointsText;


    private float timer;

    private void Start() {
        if (GameManager.Instance == null) {
            var gameManager = new GameObject("GameManager");
            gameManager.AddComponent<GameManager>();
        }

        if (pointsText == null) return;

        timer = updateInterval;
    }

    private void Update() {
        timer -= Time.deltaTime;

        if (!(timer <= 0f)) return;
        timer = updateInterval;

        var pointsToAdd = Mathf.RoundToInt(1 * 1.5F);
        GameManager.Instance.AddPoints(pointsToAdd);

        pointsText.text = $"{GameManager.Instance.GetCurrentPoints()}";
    }
}