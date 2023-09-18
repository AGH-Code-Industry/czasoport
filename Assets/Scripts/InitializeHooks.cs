#if UNITY_EDITOR

using System;
using System.Diagnostics;
using System.IO;
using CoinPackage.Debugging;
using UnityEditor;
using Debug = UnityEngine.Debug;

[InitializeOnLoad]
public static class InitializeHooks {
    private static readonly char Sep = Path.DirectorySeparatorChar;
    private static readonly string Source = $".{Sep}.coin{Sep}git-hooks{Sep}hooks";
    private static readonly string Destination = $".{Sep}.git{Sep}hooks";
    
    static InitializeHooks() {
        if (CheckIfShouldRun()) {
            Initialize();
        }
    }

    private static bool CheckIfShouldRun() {
        if (!Directory.Exists("./.coin/git-hooks")) {
            Debug.LogError("Directory .coin/git-hooks could not be found, hooks will not be initialized.");
            return false;
        }
        if (!File.Exists("./.coin/git-hooks/lastinit")) {
            Debug.Log("It seems like hooks have not been initialized on this machine yet.");
            using (StreamWriter sw = File.CreateText("./.coin/git-hooks/lastinit")) {
                sw.WriteLine(DateTime.Now);
            }
            return true;
        }
        string text = File.ReadAllText("./.coin/git-hooks/lastinit");
        if (DateTime.Parse(text).AddHours(24) < DateTime.Now) {
            Debug.Log("Hooks have been initialized more than a day ago.");
            using (StreamWriter sw = File.CreateText("./.coin/git-hooks/lastinit")) {
                sw.WriteLine(DateTime.Now);
            }
            return true;
        }
        return false;
    }

    private static void Initialize() {
        Debug.Log("Initializing hooks...");
        var files = Directory.GetFiles(Source);
        foreach (var file in files) {
            var hook = file.Split(Sep)[^1];
            var sourceFile = new FileInfo(file);
            var newFile = $"{Destination}{Sep}{hook}";
            Debug.Log($"Hook initialized: {hook}");
            sourceFile.CopyTo(newFile, true);
        }
        Debug.Log("Hook initialization finished!");
    }
}

#endif

