using System;

namespace CustomInput.Exceptions {
    public class NoMainCameraException : Exception{
        public NoMainCameraException() {}
        public NoMainCameraException(string message) : base(message) {}
        public NoMainCameraException(string message, Exception inner) : base(message, inner) {}
    }
}