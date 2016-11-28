using System;

namespace TestApplication
{
    public class Result<T>
    {
        private readonly T _value;
        private readonly bool _isValid;
        private readonly string _errorMessage = "";

        private Result(T value)
        {
            _isValid = true;
            _value = value;
        }

        private Result(string errorMessage)
        {
            _isValid = false;
            _errorMessage = errorMessage;
        }

        public T Value
        {
            get
            {
                if (!_isValid) throw new InvalidOperationException();
                return _value;
            }
        }

        public bool IsValid { get { return _isValid; } }
        public string Message { get { return _errorMessage; } }

        public static Result<T> Ok(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Error(string message)
        {
            return new Result<T>(message);
        }
    }
}
