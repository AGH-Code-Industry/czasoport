#if UNITY_EDITOR

using System;
using System.Diagnostics;
using System.IO;
using CoinPackage.Debugging;
using UnityEditor;
using Debug = UnityEngine.Debug;

[InitializeOnLoad]
public static class InitializeHooks
{
    static InitializeHooks() {
        if (CheckIfShouldRun()) {
            Initialize();
        }
    }

    private static bool CheckIfShouldRun() {
        if (!Directory.Exists("./.coin/git-hooks")) {
            CDebug.LogError("Directory .coin/git-hooks could not be found, hooks will not be initialized.");
            return false;
        }
        if (!File.Exists("./.coin/git-hooks/lastinit")) {
            return true;
        }
        string text = File.ReadAllText("./.coin/git-hooks/lastinit");
        if (DateTime.Parse(text).AddHours(12) < DateTime.Now) {
            return true;
        }
        return false;
    }

    private static void Initialize() {
        CDebug.Log("Initializing hooks...");
        Process p = new Process();
        p.StartInfo = new ProcessStartInfo("python", "./.coin/git-hooks/load-hooks.py");
        p.Start();
        while(!p.HasExited){}
    }
}

#endif

