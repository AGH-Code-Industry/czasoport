using System;
using System.Collections.Generic;
using CoinPackage.Debugging;

namespace CustomInput {
    /// <summary>
    /// Provides locking mechanism for user input. USE ONLY WITH INPUT ACTIONS
    /// </summary>
    /// <typeparam name="T">Input Actions</typeparam>
    public class InputLock<T> {
        private readonly dynamic _input;
        private readonly List<string> _keys;
        private readonly string _tag;
        private ushort _keyID;
        
        public InputLock(T input) {
            _input = input;
            _tag = _input.ToString();
            CDebug.Log(_tag);
            _keyID = 2137;
            _keys = new List<string>();
        }

        public string Lock() {
            string key = _tag + _keyID++;
            CDebug.Log("Generated key: " + key);
            _keys.Add(key);
            UpdateLock();
            return key;
        }

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