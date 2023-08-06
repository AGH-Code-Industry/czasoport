using System;
using System.Collections.Generic;
using CoinPackage.Debugging;

namespace CustomInput.Locks {
    /// <summary>
    /// Provides locking mechanism for user Inventory actions.
    /// </summary>
    public class InventoryLock {
        private readonly List<string> _keys;
        private readonly string _tag;
        
        private InputActions.InventoryActions _input;
        private ushort _keyID;
        
        public InventoryLock(InputActions.InventoryActions input) {
            _input = input;
            _tag = _input.ToString();
            CDebug.Log(_tag);
            _keyID = 2137;
            _keys = new List<string>();
        }

        /// <summary>
        /// Lets CustomInput know that current thread is not ready to receive input from user.
        /// 'Thread' safe, use instead of `Enable` and `Disable` on individual actions.
        /// </summary>
        /// <returns>Key that is needed to unlock the actions.</returns>
        public string Lock() {
            string key = _tag + _keyID++;
            CDebug.Log("Generated key: " + key);
            _keys.Add(key);
            UpdateLock();
            return key;
        }

        /// <summary>
        /// Lets CustomInput know that current thread is ready to receive input from user.
        /// </summary>
        /// <param name="key">Obtained by invoking Lock().</param>
        /// <exception cref="Exception"></exception>
        public void Unlock(string key) {
            if (_keys.Contains(key)) {
                _keys.Remove(key);
                UpdateLock();
            }
            else {
                throw new Exception("Key to unlock InputActions not found");
            }
        }

        private void UpdateLock() {
            if (_keys.Count == 0) {
                _input.Enable();
            }
            else {
                _input.Disable();
            }
            CDebug.Log(_keys.Count);
        }
    }
}