#region

using System;
using Discord;
using File_Control.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

public class DiscordRPCController : MonoBehaviour {
    private static Discord.Discord discord;
    private static bool instanceExists;

    [Header("Time Settings")] [SerializeField]
    private float statusUpdateInterval = 5f;

    private float nextStatusUpdateTime;
    private long time;

    private void Awake() {
        if (!instanceExists) {
            instanceExists = true;

            if (transform.parent != null) transform.SetParent(null);

            DontDestroyOnLoad(gameObject);
        }
        else if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        discord = new Discord.Discord(GameManager.GetRPCKey(), (ulong)CreateFlags.Default);
        time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    private void Update() {
        try {
            discord.RunCallbacks();
        }
        catch {
            Destroy(gameObject);
        }

        if (Time.time >= nextStatusUpdateTime) {
            UpdateStatus();
            nextStatusUpdateTime = Time.time + statusUpdateInterval;
        }
    }

    private void OnApplicationQuit() {
        ClearPresence();

        try {
            discord?.RunCallbacks();
        }
        catch (Exception e) {
            Logging.LogError($"Error running Discord callbacks on quit: {e.Message}");
        }

        if (discord != null) {
            discord.Dispose();
            discord = null;
        }
    }


    private void UpdateStatus() {
        try {
            var activityManager = discord.GetActivityManager();
            Activity activity = new() {
                Details = GetDetailsScene(),
                State = currentState(),
                Assets = {
                    LargeImage = GameManager.GetLargeImage(),
                    LargeText = GameManager.GetLargeText()
                },
                Timestamps = {
                    Start = time
                }
            };

            activityManager.UpdateActivity(activity, res => {
                if (res != Result.Ok) Logging.LogWarning("Failed connecting to Discord, Result: " + res);
            });
        }
        catch {
            Destroy(gameObject);
        }
    }


    private static void ClearPresence() {
        if (discord == null) return;

        var activityManager = discord.GetActivityManager();
        activityManager.ClearActivity(res => {
            if (res != Result.Ok) Logging.LogError("Failed to clear Discord presence! " + res);
        });
    }

    private string GetDetailsScene() {
        var detail = "In Game";

#if UNITY_EDITOR
        detail = SceneManager.GetActiveScene().name switch {
            "Menu" => "Debugging Main Menu",
            "Game" => "Debugging The Mines",
            _ => "In Game (Editor)"
        };

#elif UNITY_STANDALONE
        detail = SceneManager.GetActiveScene().name switch {
            "Menu" => "In the Main Menu",
            "Game" => "Playing The Game!",
            _ => "In Game"
        };
#endif

        return detail;
    }


    private string currentState() {
        return GameManager.GetStage() + " v" + GameManager.GetVersion();
    }
}