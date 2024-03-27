using Application;
using CoinPackage.Debugging;
using UnityEngine;

namespace Utils.Singleton {
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
        public static T I { get; private set; }

        private static readonly CLogger Logger = Loggers.LoggersList[Loggers.LoggerType.UTILS];

        protected virtual void Awake() {
            if (I != null) {
                throw new SingletonOverrideException($"Tried to overwrite {typeof(T)} singleton.");
            }
            I = FindObjectOfType<T>();
            Logger.Log($"Created singleton instance for {typeof(T) % Colorize.Yellow}", gameObject);
        }
    }
}