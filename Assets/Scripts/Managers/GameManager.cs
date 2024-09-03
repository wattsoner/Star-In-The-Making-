using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GameManager : MonoBehaviour {
    // Game Information
    private const string GAME_NAME = "Star In The Making";
    private const string STUDIO = "Chomp Studios";

    // Game Versioning
    private const string VERSION = "0.0.1";

    // Encryption Keys
    private const string KEY_32 = "";
    private const string KEY_16 = "";

    // Discord RPC variables
    private const long DISCORD_RPC_KEY = 1277617743855751279;

    private const string DISCORD_LARGE_TEXT = "";
    private const string DISCORD_LARGE_IMAGE = "";


    private static DevelopmentStage _currentStage;

    private int _currentPoints;
    private int _highScore;

    private bool _isInitialized;

    public static GameManager Instance { get; private set; }

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
        if (_isInitialized) return;
        
        _currentStage = DevelopmentStage.Development;

        _currentPoints = 0;
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
            
        _isInitialized = true;
    }
    
    

    public void AddPoints(int points) {
        _currentPoints += points;
        if (_currentPoints <= _highScore) return;

        _highScore = _currentPoints;
        PlayerPrefs.SetInt("HighScore", _highScore);
        PlayerPrefs.Save();
    }

    public int GetCurrentPoints() {
        return _currentPoints;
    }

    public int GetHighScore() {
        return _highScore;
    }

    public void ResetPoints() {
        _currentPoints = 0;
    }

    public static string GetStage() {
        return _currentStage.ToString();
    }

    public static string GetKey32() {
        return KEY_32;
    }

    public static string GetKey16() {
        return KEY_16;
    }

    public static string GetGameName() {
        return GAME_NAME;
    }

    public static string GetStudio() {
        return STUDIO;
    }

    public static string GetVersion() {
        return VERSION;
    }

    public static long GetRPCKey() {
        return DISCORD_RPC_KEY;
    }

    public static string GetLargeText() {
        return DISCORD_LARGE_TEXT;
    }

    public static string GetLargeImage() {
        return DISCORD_LARGE_IMAGE;
    }

    private enum DevelopmentStage {
        Development,
        Alpha,
        Beta,
        Testing,
        Release
    }
}