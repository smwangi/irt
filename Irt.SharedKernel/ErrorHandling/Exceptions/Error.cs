using System.Net;
using Irt.SharedKernel.ErrorHandling.Exceptions;

namespace Irt.SharedKernel.ErrorHandling.Exceptions;

public class Error
{
    public string Code { get; }
    public string Message { get; }
    public HttpStatusCode StatusCode { get; }
    public IDictionary<string, object> Details { get; }
    
    protected Error(
        string code, 
        string message, 
        HttpStatusCode statusCode, 
        IDictionary<string, object>? details = null)
    {
        Code = code;
        
        Message = message;
        StatusCode = statusCode;
        Details = details ?? new Dictionary<string, object>();
    }
    
    public static Error FromException(AppException appException)
        => new(
            appException.ErrorCode,
            appException.Message,
            appException.StatusCode,
            appException.Details);
    
    public static Error Unexpected(
        string message = "An unexpected error occurred.",
        string errorCode = "INTERNAL_ERROR",
        IDictionary<string, object>? details = null,
        Exception? innerException = null)
    {
        return new Error(
            errorCode,
            message,
            HttpStatusCode.InternalServerError,
            details);
    }
    
    public static Error Validation(string msg) 
        => new("VALIDATION_ERROR", msg, HttpStatusCode.BadRequest);
    public static Error NotFound(string msg) 
        => new("NOT_FOUND", msg, HttpStatusCode.NotFound);
    public static Error Unexpected(string msg) 
        => new("UNEXPECTED_ERROR", msg, HttpStatusCode.InternalServerError);
}