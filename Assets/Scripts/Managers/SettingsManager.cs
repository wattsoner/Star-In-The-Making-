using UnityEngine;

public class SettingsManager : MonoBehaviour {
    public static SettingsManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;

            if (transform.parent != null) transform.SetParent(null);

            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}