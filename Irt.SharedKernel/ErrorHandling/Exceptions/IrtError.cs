using System.Net;
using Irt.SharedKernel.ErrorHandling.Exceptions;

namespace Irt.SharedKernel.ErrorHandling.Exceptions;

public class IrtError
{
    public string Code { get; }
    public string Message { get; }
    public HttpStatusCode StatusCode { get; }
    public IDictionary<string, object> Details { get; }
    public string Type { get; set; }
    
    public IrtError(string type, string message, string? code = null, Dictionary<string, object>? details = null)
    {
        Type = type;
        Message = message;
        Code = code;
        Details = details;
    }
    
    protected IrtError(
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
    
    public static IrtError FromException(AppException appException)
        => new(
            appException.ErrorCode,
            appException.Message,
            appException.StatusCode,
            appException.Details);
    
    public static IrtError Unexpected(
        string message = "An unexpected error occurred.",
        string errorCode = "INTERNAL_ERROR",
        IDictionary<string, object>? details = null,
        Exception? innerException = null)
    {
        return new IrtError(
            errorCode,
            message,
            HttpStatusCode.InternalServerError,
            details);
    }
    
    public static IrtError Validation(string msg) 
        => new("VALIDATION_ERROR", msg, HttpStatusCode.BadRequest);
    public static IrtError NotFound(string msg) 
        => new("NOT_FOUND", msg, HttpStatusCode.NotFound);
    public static IrtError Unexpected(string msg) 
        => new("UNEXPECTED_ERROR", msg, HttpStatusCode.InternalServerError);
    // Factory methods for common error types
    public static IrtError NotFound(string message, string? code = null) 
        => new("NotFound", message, code);
        
    public static IrtError Validation(string message, string? code = null) 
        => new("Validation", message, code);
        
    public static IrtError Unauthorized(string message, string? code = null) 
        => new("Unauthorized", message, code);
        
    public static IrtError Internal(string message, string? code = null) 
        => new("Internal", message, code);
        
    public static IrtError BadRequest(string message, string? code = null) 
        => new("BadRequest", message, code);
        
    public static IrtError Forbidden(string message, string? code = null) 
        => new("Forbidden", message, code);
        
    public static IrtError Conflict(string message, string? code = null) 
        => new("Conflict", message, code);
}