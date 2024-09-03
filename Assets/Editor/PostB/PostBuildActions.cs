#region

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using CompressionLevel = System.IO.Compression.CompressionLevel;

#endregion

public class PostBuildActions {
    
    [PostProcessBuild(1)]
    public static void OnPostprocessBuild(BuildTarget target, String pathToBuiltProject) {
        var buildDir = Path.GetDirectoryName(pathToBuiltProject);
        if (buildDir != null) {
            var extrasFolder = Path.Combine(buildDir, "Extras");
            
            if (!Directory.Exists(extrasFolder)) {
                Directory.CreateDirectory(extrasFolder);
            }
            
            var sourceFolder = Path.Combine(buildDir, "../Design/Music Files");
            var mp3FileName1 = "Star In The Making.mp3";
            //  var mp3FileName2 = "The Miner's Prelude.mp3";
            
            var sourceFile1 = Path.Combine(sourceFolder, mp3FileName1);
            //  var sourceFile2 = Path.Combine(sourceFolder, mp3FileName2);
            
            if (File.Exists(sourceFile1)) {
                File.Copy(sourceFile1, Path.Combine(extrasFolder, mp3FileName1), overwrite: true);
            }
            
            //  if (File.Exists(sourceFile2)) {
            //      File.Copy(sourceFile2, Path.Combine(extrasFolder, mp3FileName2), overwrite: true);
            //  }
            
            var helpGuideContent = GenerateHelpGuideContent();
            var helpGuideFilePath = Path.Combine(extrasFolder, "Guide.txt");
            File.WriteAllText(helpGuideFilePath, helpGuideContent);
        }

        var creditsContent = GenerateDefaultCredits();

        if (buildDir != null) {
            var creditsFilePath = Path.Combine(buildDir, "Extras/Credits.txt");
            File.WriteAllText(creditsFilePath, creditsContent);
        }

        var timestamp = DateTime.Now.ToString("dd-MM-yy_HH-mm");
        var version = GameManager.GetVersion(); // Get the version from GameManager
        var versionedZipFolder = Path.Combine(buildDir, $"../Archive/Zips/{version}");

        // Ensure the versioned folder exists
        if (!Directory.Exists(versionedZipFolder)) {
            Directory.CreateDirectory(versionedZipFolder);
        }

        var zipFileName = GameManager.GetGameName() + " [" + GameManager.GetStage().ToLower() + "]" + " v" + version + " (" + timestamp + ")" + ".zip";
        var zipFilePath = Path.Combine(buildDir, zipFileName);

        try {
            ZipDirectory(buildDir, zipFilePath, "Star In The Making_BackUpThisFolder_ButDontShipItWithYourGame", "Star In The Making_BurstDebugInformation_DoNotShip", "Star In The Making_Data");
            Debug.Log("Build archived successfully: " + zipFilePath);

            if (File.Exists(Path.Combine(buildDir, "Extras/Credits.txt"))) {
                File.Delete(Path.Combine(buildDir, "Extras/Credits.txt"));
                Debug.Log("Credits.txt deleted.");
            }

            if (File.Exists("Extras/Guide.txt")) {
                File.Delete("Extras/Guide.txt");
                Debug.Log("Guide.txt deleted.");
            }
            
            if (Directory.Exists(Path.Combine(buildDir, "Extras"))) {
                Directory.Delete(Path.Combine(buildDir, "Extras"), true);
                Debug.Log("Extras folder deleted.");
            }

            var archiveFilePath = Path.Combine(versionedZipFolder, zipFileName);
            
            File.Move(zipFilePath, archiveFilePath);
            Debug.Log("Zip file moved to: " + archiveFilePath);
        }
        catch (Exception ex) {
            Debug.LogError("Failed to archive build: " + ex.Message);
        }
    }

    private static void ZipDirectory(String sourceDir, String zipFilePath, params String[] excludeFolders) {
        List<String> files = new List<String>(Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories));
        
        files.RemoveAll(path => excludeFolders.Any(path.Contains) || Path.GetExtension(path).Equals(".zip", StringComparison.OrdinalIgnoreCase));

        using (FileStream zipToOpen = new(zipFilePath, FileMode.Create)) {
            using (ZipArchive archive = new(zipToOpen, ZipArchiveMode.Create)) {
                foreach (String file in files) {
                    String entryName = file.Substring(sourceDir.Length + 1);
                    archive.CreateEntryFromFile(file, entryName, CompressionLevel.Optimal);
                }
            }
        }
    }

    private static String GenerateHelpGuideContent() {
        return "GUIDE FOR Star In The Making\n\n" +
               "----------------------------------------\n" +
               "KEYBINDS\n" +
               "----------------------------------------\n" +
               "- \n" +
               "- \n" +
               "- \n\n" +
               "Desc.\n\n" +
               "----------------------------------------\n" +
               "GENERAL TIPS\n" +
               "----------------------------------------\n" +
               "- \n" +
               "- \n" +
               "- \n" +
               "- \n" +
               "- \n" +
               "- \n" +
               "- Desc\n\n" +
               "----------------------------------------\n" +
               "EXTRAS\n" +
               "----------------------------------------\n" +
               "- The Game's soundtrack has also been included, feel free to listen & enjoy it (c).\n" +
               "----------------------------------------\n" +
               "\n" +
               "----------------------------------------\n" +
               "Good luck, and enjoy playing Star In The Making!";
    }


    private static String GenerateDefaultCredits() {
        const String sharkASCII =
            "                             ,_.\n" +
            "                           ./  |                                          _-\n" +
            "                         ./    |                                       _-'/\n" +
            "      ______.,         ./      /                                     .'  (\n" +
            " _---'___._.  '----___/       (                                    ./  /`'\n" +
            "(,----,_  G \\                  \\_.                               ./   :\n" +
            " \\___   \"--_                      \"--._,                       ./    /\n" +
            " /^^^^^-__          ,,,,,               \"-._       /|         /     /\n" +
            " `,       -        /////                    \"`--__/ (_,    ,_/    ./\n" +
            "   \"-_,           ''''' __,                            `--'      /\n" +
            "       \"-_,             \\\\ `-_                                  (\n" +
            "           \"-_.          \\\\   \\.                                 \\_\n" +
            "          /    \"--__,      \\\\   \\.                       ____.     \"-._,\n" +
            "         /        ./ `---____\\\\   \\.______________,---\\ (     \\,        \"-.,\n" +
            "        |       ./             \\\\   \\        /\\  |     \\|       `--______---`\n" +
            "        |     ./                 \\\\  \\      /_/\\_!\n" +
            "        |   ./                     \\\\ \\\n" +
            "        |  /    *:CHOMP STUDIOS:*    \\_\\\n" +
            "        |_/\n\n";

        return sharkASCII +
               "ESCAPE THE MINES\n\n" +
               "----------------------------------------\n" +
               "CREDITS\n" +
               "----------------------------------------\n\n" +
               "Game By Wattie, Chomp Studios\n\n" +
               "----------------------------------------\n" +
               "ASSET PACKS\n" +
               "----------------------------------------\n" +
               "- 6000+ Flat Buttons Icons Pack: https://assetstore.unity.com/packages/2d/gui/icons/6000-flat-buttons-icons-pack-64223\n" +
               "- Particle Backgrounds: https://assetstore.unity.com/packages/vfx/particles/particle-backgrounds-177320\n" +
               "- Animated UI Buttons: https://2pairnoise.itch.io/animated-hud-media-control-icon-pack\n" +
               "----------------------------------------\n" +
               "MUSIC\n" +
               "----------------------------------------\n" +
               "- 'Star In The Making - 8 Bit'\n\n" +
               "----------------------------------------\n" +
               "ARTWORK\n" +
               "----------------------------------------\n" +
               "- Tehzo: Album Art, Game Icon(s), Discord Icon(s) \n" +
               "- \n\n" +
               "----------------------------------------\n" +
               "SPECIAL THANKS\n" +
               "----------------------------------------\n" +
               "- JuiceMage: UI Help & Inspiration" +
               "- Duck: UI Help & Inspiration" +
               "- SleepyJack: Support With Line Drawing\n\n" +
               "© Chomp Studios\n" +
               "All rights reserved.";
    }
    
}
