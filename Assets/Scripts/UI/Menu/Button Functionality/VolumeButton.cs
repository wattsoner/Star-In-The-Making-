using UnityEngine;

public class VolumeButton : MonoBehaviour {
    private bool isMuted;

    public void OnClick() {
        switch (isMuted) {
            case true:
                AudioManager.Instance.UnmuteAudio();
                break;

            case false:
                AudioManager.Instance.MuteAudio();
                break;
        }

        isMuted = !isMuted;
    }
}