
namespace Irt.Application.Configuration.Results
{
    public class Result<TResponse, TError>
    {
        public TResponse Response { get; }
        public bool IsSuccess { get; }
        public TError? Error { get; }
        public Dictionary<string, object> Metadata { get; }

        // Private constructor to enforce usage of static factory methods
        private Result(
            TResponse response,
            TError? error,
            bool isSuccess, 
            Dictionary<string, object>? metadata = null)
        {
            IsSuccess = isSuccess;
            Response = response;
            Error = error ?? default;
            Metadata = metadata ?? new Dictionary<string, object>();
        }

        public static Result<TResponse, TError> Success(TResponse response, Dictionary<string, object> metadata = null)
        {
            return new Result<TResponse, TError>(response: response, default, true, metadata: metadata);
        }

        public static Result<TResponse, TError> Success(TResponse response, string metadataKey, object metadataValue)
        {
            var metadata = new Dictionary<string, object> { { metadataKey, metadataValue } };
            return new Result<TResponse, TError>(response: response, default, true, metadata: metadata);
        }

        public static Result<TResponse, TError> Success(TResponse response, bool wasUpdated)
        {   
            return Success(response, "WasUpdated", wasUpdated);
        }
        
        public static Result<TResponse, TError?> Failure(TError? error, Dictionary<string, object> metadata = null)
        {
            return new Result<TResponse, TError?>(default, error, false, metadata);
        }
        
        public static Result<TResponse, List<TError?>> Failure(List<TError?> errors, Dictionary<string, object> metadata = null)
        {
            return new Result<TResponse, List<TError?>>(default, errors, false, metadata);
        }

        // Helper method to check and retrieve metadata
        public bool TryGetMetadata<T>(string key, out T value)
        {
            if (Metadata.TryGetValue(key, out var objValue) && objValue is T typedValue)
            {
                value = typedValue;
                return true;
            }

            value = default;
            return false;
        }

        public T GetMetadata<T>(string key, T defaultValue = default)
        {
            return TryGetMetadata<T>(key, out var value) ? value : defaultValue;
        }

        public bool WasUpdated => GetMetadata<bool>("WasUpdated", false);
    }
}