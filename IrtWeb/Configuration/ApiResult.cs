namespace IrtWeb.Configuration;

public class ApiResult<T>
{
    public bool IsSuccess { get; private set; }
    public T Data { get; private set; }
    public string Error { get; private set; }
    public Dictionary<string, object> Metadata { get; private set; }
    
    public ApiResult(bool isSuccess, T data, string error = null, Dictionary<string, object> metadata = null)
    {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
        Metadata = metadata ?? new Dictionary<string, object>();
    }
    
    public static ApiResult<T> Success(T data, Dictionary<string, object> metadata = null)
    {
        return new ApiResult<T>(true, data, null, metadata);
    }
    
    public static ApiResult<T> Success(T data, string metadataKey, object metadataValue)
    {
        var metadata = new Dictionary<string, object> { { metadataKey, metadataValue } };
        return new ApiResult<T>(true, data, null, metadata);
    }
    
    public static ApiResult<T> Failure(string error, Dictionary<string, object> metadata = null)
    {
        return new ApiResult<T>(false, default, error, metadata);
    }
}