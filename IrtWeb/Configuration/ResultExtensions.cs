using Irt.Application.Configuration.Results;

namespace IrtWeb.Configuration;

public static class ResultExtensions
{
    public static ApiResult<TResponse> ToApiResult<TResponse, TError, TMetadata>(
        this Result<TResponse, TError> result)
    {
        if (result.IsSuccess)
        {
            return ApiResult<TResponse>.Success(result.Response, result.Metadata);
        }
        else
        { 
            return ApiResult<TResponse>.Failure(result.Error?.ToString(), result.Metadata);
        }
    }
    
    // For string errors (most common case)
    public static ApiResult<TResponse> ToApiResult<TResponse>(
        this Result<TResponse, string> result)
    {
        if (result.IsSuccess)
        {
            return ApiResult<TResponse>.Success(result.Response, result.Metadata);
        }
        else
        {
            return ApiResult<TResponse>.Failure(result.Error ?? "Unknown error", result.Metadata);
        }
    }

    // For any reference type errors
    public static ApiResult<TResponse> ToApiResult<TResponse, TError>(
        this Result<TResponse, TError> result) where TError : class
    {
        if (result.IsSuccess)
        {
            return ApiResult<TResponse>.Success(result.Response, result.Metadata);
        }
        else
        {
            return ApiResult<TResponse>.Failure(result.Error?.ToString() ?? "Unknown error", result.Metadata);
        }
    }
}