using System;

namespace Application.GlobalExceptions {
    public class SingletonOverrideException : Exception {
        public SingletonOverrideException() { }
        public SingletonOverrideException(string message) : base(message) { }
        public SingletonOverrideException(string message, Exception inner) : base(message, inner) { }
    }
}