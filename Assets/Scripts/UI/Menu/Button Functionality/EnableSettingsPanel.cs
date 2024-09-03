using UnityEngine;

public class EnableSettingsPanel : MonoBehaviour {
    public GameObject panelToToggle;

    public void OnButtonClick() {
        if (panelToToggle != null) {
            var isActive = panelToToggle.activeSelf;
            panelToToggle.SetActive(!isActive);
        }
        else {
            Debug.LogWarning("Panel to toggle is not assigned.");
        }
    }
}