namespace Irt.Application.Configuration.ApiResponse
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class ApiResponse<T>
    {
        [JsonPropertyName("message")]
        public string Message { get; }

        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; }

        [JsonPropertyName("data")]
        public T? Data { get; }

        [JsonPropertyName("errors")]
        public List<ApiError> Errors { get; }

        [JsonPropertyName("metadata")]
        public Dictionary<string, object> Metadata { get; }

        private ApiResponse(
            bool isSuccess,
            string message,
            T? data = default(T),
            List<ApiError> errors = null,
            Dictionary<string, object> metadata = null)
        {
            this.Message = message;
            this.Data = data;
            this.IsSuccess = isSuccess;
            Errors = errors ?? new List<ApiError>();
            Metadata = metadata ?? new Dictionary<string, object>();
        }

        public static ApiResponse<T> Success(T data, string? message = null)
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> Fail(string message)
        {
            return new ApiResponse<T>(false, message);
        }

        // Static factory methods for convenience
        public static ApiResponse<T> Success(T data, Dictionary<string, object> metadata = null)
        {
            return new ApiResponse<T>(true, "success", data, null, metadata);
        }

        public static ApiResponse<T> Error(List<ApiError> errors, Dictionary<string, object> metadata = null)
        {
            return new ApiResponse<T>(false, "error", default, errors, metadata);
        }

        public static ApiResponse<T> Error(ApiError error, Dictionary<string, object> metadata = null)
        {
            return new ApiResponse<T>(false, "error", default, new List<ApiError> { error }, metadata);
        }

        public ApiResponse<T> AddError(ApiError error)
        {
            Errors.Add(error);
            return this;
        }

        public ApiResponse<T> AddMetadata(string key, object value)
        {
            Metadata[key] = value;
            return this;
        }

        /**
         * var response = ApiResponse<object>.Error("Validation error")
            .AddError("Another error")
            .AddMetadata("Timestamp", DateTime.UtcNow);

            var errors = new List<ApiError>
            {
                new ApiError { Code = "ERR001", Message = "Validation failed" },
                new ApiError { Code = "ERR002", Message = "Resource not found" }
            };

            var response = ApiResponse<object>.Error(errors);

         */
    }

    public class ApiError
    {
        public ApiError(string message)
        {
            Message = message;
        }
        public string Message { get; }
        public string Code { get; set; }
        public string Target { get; set; }
    }
}