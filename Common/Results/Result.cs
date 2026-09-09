namespace exam_system.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Result(bool isSuccess, string errorMessage )
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
        public static Result Success() => new (true, string.Empty);
        public static Result Failure(string error)=> new(false, error);
    }
}
