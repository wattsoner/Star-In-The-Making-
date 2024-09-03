using System;
using System.IO;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Targets;
using UnityEngine;
using Logger = NLog.Logger;

namespace File_Control.Logging {
    public static class Logging {
        private static readonly Logger logger;

        static Logging() {
            try {
                // Enable NLog internal logging
                InternalLogger.LogFile = Path.Combine(Application.persistentDataPath, "NLog-internal.log");
                InternalLogger.LogLevel = LogLevel.Trace;

                // Ensure the logs directory exists
                var logDir = Path.Combine(Application.persistentDataPath, "Logs");
                if (!Directory.Exists(logDir)) {
                    Directory.CreateDirectory(logDir);
                    Debug.Log($"Created log directory at {logDir}");
                }

                var config = new LoggingConfiguration();

                var fileTarget = new FileTarget("file") {
                    FileName = Path.Combine(logDir, "star.log"),
                    Layout =
                        "${longdate} | ${uppercase:${level}} | ${logger} | ${message} | ${exception:format=tostring} | ${callsite:className=true:methodName=true:fileName=true:includeSourcePath=false} | Thread ID: ${threadid}",
                    DeleteOldFileOnStartup = true,
                    ConcurrentWrites = true,
                    KeepFileOpen = false
                };
                config.AddTarget(fileTarget);
                config.AddRuleForAllLevels(fileTarget);

                LogManager.Configuration = config;

                logger = LogManager.GetCurrentClassLogger();
                Debug.Log("NLog configuration loaded successfully.");
            }
            catch (Exception ex) {
                Debug.LogError($"Error initializing LogUtil: {ex.Message}");
            }
        }

        public static void LogInfo(string message) {
            logger.Info(message);
        }

        public static void LogWarning(string message) {
            logger.Warn(message);
        }

        public static void LogError(string message) {
            logger.Error(message);
        }

        public static void LogDebug(string message) {
            logger.Debug(message);
        }

        public static void LogException(Exception exception) {
            logger.Error(exception);
        }
    }
}