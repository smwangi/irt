namespace Irt.Application.Configuration.Results
{
    public class UpdateResult<T>
    {
        public bool IsSuccess { get; }
        public T? Data { get; }
        public string? Message { get; }

        private UpdateResult(bool isSuccess, string? message = null, T? data = default)
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
        }

        public static UpdateResult<T> Success(T? data, string? message = null) => new(true, message, data);
        public static UpdateResult<T> Fail(string? message) => new(false, message);
    }
}