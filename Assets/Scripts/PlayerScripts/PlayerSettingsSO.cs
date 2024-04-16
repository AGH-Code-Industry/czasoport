using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts {

    /// <summary>
    /// Settings for scene changing and time changing systems.
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings/PlayerSettings")]
    public class PlayerSettingsSO : ScriptableObject {
        public float movementSpeed;
    }
}