namespace PlanYourTravel.Application.Commons
{
    /// <summary>
    /// Represen a non-typed result of an operation.
    /// If <see cref="IsSuccess"/> is false, see <see cref="Error"/>
    /// </summary>
    public class Result
    {
        private readonly Error? _error;

        protected Result(bool isSuccess, Error? error)
        {
            IsSuccess = isSuccess;
            _error = error;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error? Error => IsFailure ? _error : null;

        public static Result Success()
            => new(true, null);

        public static Result Failure(Error error)
            => new(false, error);

        public static Result<TValue> Success<TValue>(TValue value)
            => new(value, true, null);

        public static Result<TValue> Failure<TValue>(Error error)
            => new(default, false, error);
    }

    /// <summary>
    /// Represent a typed result of an operation. IF <see cref="IsSuccess"/> is false,
    /// the <see cref="Value"/> should not be accessed.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        internal Result(TValue? value, bool isSuccess, Error? error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Cannot access the value of a failed result.");

        public static implicit operator Result<TValue>(TValue value)
            => new(value, true, null);
    }

    /// <summary>
    /// Represent an error of an operation.
    /// </summary>
    public class Error
    {
        public string Code { get; }
        public string Message { get; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public override string ToString() => $"{Code} {Message}";
    }
}
