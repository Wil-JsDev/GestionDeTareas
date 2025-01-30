namespace GestionDeTareas.Core.Domain.Utils
{
    public class Result
    {
        public bool IsSuccess { get; set; }

        public Error? Error { get; set; }

        protected Result()
        {
            IsSuccess = true;
            Error = default;
        }

        protected Result(Error error)
        {
            IsSuccess = false;
            Error = error;
        }

        public static Result Success() =>
           new();

        public static Result Failure(Error error) =>
            new(error);
    }

    public sealed class ResultT<TValue> : Result
    {
        private readonly TValue _value;

        private ResultT(TValue value) : base()
        {
            _value = value;
        }

        public static implicit operator ResultT<TValue>(Error error) =>
            new(error);

        public static implicit operator ResultT<TValue>(TValue value) =>
            new(value);

        private ResultT(Error error) : base(error)
        {
            _value = default;
        }

        public static ResultT<TValue> Success(TValue value) =>
            new(value);

        public static ResultT<TValue> Failure(Error error) =>
            new(error);
    }
}
