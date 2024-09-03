using UnityEngine;

public class AudioManager : MonoBehaviour {
    [Header("Audio Source Attachables")] [SerializeField]
    private AudioSource musicSource;

    [SerializeField] private AudioSource effectsSource;

    private bool _isInitialized;

    private bool isMuted;
    public static AudioManager Instance { get; private set; }

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
        if (!_isInitialized) {
            // Perform all initialization here
            if (musicSource == null || effectsSource == null)
                Debug.LogError("AudioSources are not assigned in the AudioManager!");

            if (musicSource == null) musicSource = gameObject.AddComponent<AudioSource>();

            if (effectsSource == null) effectsSource = gameObject.AddComponent<AudioSource>();

            // Mark as initialized
            _isInitialized = true;
        }
    }

    public void PlayMusic(AudioClip clip) {
        if (!isMuted) {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public void PlayEffect(AudioClip clip) {
        if (!isMuted) effectsSource.PlayOneShot(clip);
    }

    public void StopMusic() {
        musicSource.Stop();
    }

    public void StopEffect() {
        effectsSource.Stop();
    }

    public bool IsMusicPlaying() {
        return musicSource.isPlaying;
    }

    public bool IsEffectPlaying() {
        return effectsSource.isPlaying;
    }

    public void MuteAudio() {
        isMuted = true;
        musicSource.Pause();
        effectsSource.Pause();
    }

    public void UnmuteAudio() {
        isMuted = false;
        musicSource.UnPause();
        effectsSource.UnPause();
    }

    public void ToggleAudio() {
        if (isMuted)
            UnmuteAudio();
        else
            MuteAudio();
    }

    public void SetMusicVolume(float volume) {
        musicSource.volume = volume;
    }

    public void SetEffectsVolume(float volume) {
        effectsSource.volume = volume;
    }
}