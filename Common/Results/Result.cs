namespace exam_system.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsFailure => !IsSuccess;
        public Result(bool isSuccess, string errorMessage )
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
        public static Result Success() => new (true, string.Empty);
        public static Result Failure(string error)=> new(false, error);
    }
    public class Result<T> : Result
    {
        public T? Value { get; set; }
        public Result(bool isSuccess, string errorMessage, T? value = default)
            : base(isSuccess, errorMessage)
        
           => Value = value;
        public static Result<T> Success(T value) => new(true, string.Empty, value);
        public static new Result<T> Failure(string error) => new(false, error, default);
    }
}
