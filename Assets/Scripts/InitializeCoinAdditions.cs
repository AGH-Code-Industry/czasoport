#if UNITY_EDITOR

using System;
using System.Diagnostics;
using System.IO;
using CoinPackage.Debugging;
using UnityEditor;
using Debug = UnityEngine.Debug;


[InitializeOnLoad]
public static class InitializeCoinAdditions
{
    static InitializeCoinAdditions() {
        if (CheckIfShouldRun()) {
            Initialize();
        }
    }

    private static bool CheckIfShouldRun() {
        if (!Directory.Exists("./.coin")) {
            CDebug.LogError("Coin Additions are missing!");
            return false;
        }
        if (!File.Exists("./.coin/lastinit")) {
            return true;
        }
        string text = File.ReadAllText("./.coin/lastinit");
        if (DateTime.Parse(text).AddHours(1) < DateTime.Now) {
            return true;
        }
        return false;
    }

    private static void Initialize() {
        CDebug.Log("Running Coin Additions.");
        Process p = new Process();
        p.StartInfo = new ProcessStartInfo("python", "./.coin/coin-initializer.py");
        p.Start();
        while(!p.HasExited){}
    }
}

#endif

