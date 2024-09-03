using UnityEngine;

public class IntegrationManager : MonoBehaviour {
    private bool _isInitialized;
    public static IntegrationManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;

            if (transform.parent != null) transform.SetParent(null);

            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Initialize() {
        if (!_isInitialized) _isInitialized = true;
    }
}