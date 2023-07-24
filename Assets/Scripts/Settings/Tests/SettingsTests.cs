using System;
using System.Collections;
using System.Collections.Generic;
using CoinPackage.Debugging;
using Settings;
using UnityEngine;

public class SettingsTests : MonoBehaviour
{
    private void Start() {
        CDebug.Log(DeveloperSettings.Instance.tpcSettings.offsetFromPresentPlatform);
    }
}
