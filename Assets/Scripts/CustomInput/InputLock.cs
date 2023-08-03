using System;
using System.Collections.Generic;
using CoinPackage.Debugging;

namespace CustomInput {
    public class InputLock<T> {
        private readonly dynamic _input;
        private readonly List<int> _keys;
        private ushort _keyID;
        
        public InputLock(T input) {
            if (HasMethod(input, "Enable") && HasMethod(input, "Disable")) {
                _input = input;
            }
            else {
                throw new ArgumentException("Argument provided to InputLock is not an InputActions.");
            }
            _keyID = 2137;
            _keys = new List<int>();
        }

        public int Lock() {
            int key = _keyID++;
            _keys.Add(key);
            UpdateLock();
            return key;
        }

        public void Unlock(int key) {
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
        
        private static bool HasMethod(object objectToCheck, string methodName)
        {
            var type = objectToCheck.GetType();
            return type.GetMethod(methodName) != null;
        } 
    }
}