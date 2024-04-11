using CoinPackage.Debugging;
using UnityEngine;
using Utils;

namespace LevelTimeChange.LevelsLoader {
    /// <summary>
    /// Stores information about one 'platform' in the game.
    /// </summary>
    [CreateAssetMenu(fileName = "New Level", menuName = "Definition/New Level")]
    public class LevelInfoSO : UniqueScriptableObject {
        [Tooltip("Name of the scene object associated to this level.")]
        public string sceneName;

        [Tooltip("All levels to which player can get from this level.")]
        public LevelInfoSO[] neighbourLevels;

        public override string ToString() {
            return $"[Level: {sceneName}]" % Colorize.Magenta;
        }
    }
}