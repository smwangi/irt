using System.Net;

namespace Irt.SharedKernel.ErrorHandling.Exceptions;

public class IrtError
{
    public string Code { get; }
    public string Message { get; }
    public HttpStatusCode StatusCode { get; }
    public IDictionary<string, object> Details { get; }
    public string Type { get; }
    
    public IrtError(
        string type,
        string message,
        HttpStatusCode statusCode,
        string? code = null,
        IDictionary<string, object>? details = null)
    {
        Type = type;
        Message = message;
        StatusCode = statusCode;
        Code = code ?? DefaultCodeFor(type);
        Details = details ?? new Dictionary<string, object>();
    }
    
    public static IrtError FromException(AppException appException)
        => new(
            TypeFor(appException.StatusCode),
            appException.Message,
            appException.StatusCode,
            appException.ErrorCode,
            appException.Details);
    
    public static IrtError Unexpected(
        string message = "An unexpected error occurred.",
        string errorCode = "INTERNAL_ERROR",
        IDictionary<string, object>? details = null)
    {
        return new IrtError(
            "Internal",
            message,
            HttpStatusCode.InternalServerError,
            errorCode,
            details);
    }

    // Factory methods for common error types
    public static IrtError NotFound(string message, string? code = null, IDictionary<string, object>? details = null)
        => new("NotFound", message, HttpStatusCode.NotFound, code, details);
        
    public static IrtError Validation(string message, string? code = null, IDictionary<string, object>? details = null)
        => new("Validation", message, HttpStatusCode.BadRequest, code, details);
        
    public static IrtError Unauthorized(string message, string? code = null, IDictionary<string, object>? details = null)
        => new("Unauthorized", message, HttpStatusCode.Unauthorized, code, details);
        
    public static IrtError Internal(string message, string? code = null, IDictionary<string, object>? details = null)
        => new("Internal", message, HttpStatusCode.InternalServerError, code, details);
        
    public static IrtError BadRequest(string message, string? code = null, IDictionary<string, object>? details = null)
        => new("BadRequest", message, HttpStatusCode.BadRequest, code, details);
        
    public static IrtError Forbidden(string message, string? code = null, IDictionary<string, object>? details = null)
        => new("Forbidden", message, HttpStatusCode.Forbidden, code, details);
        
    public static IrtError Conflict(string message, string? code = null, IDictionary<string, object>? details = null)
        => new("Conflict", message, HttpStatusCode.Conflict, code, details);

    private static string TypeFor(HttpStatusCode statusCode)
        => statusCode switch
        {
            HttpStatusCode.BadRequest => "BadRequest",
            HttpStatusCode.Unauthorized => "Unauthorized",
            HttpStatusCode.Forbidden => "Forbidden",
            HttpStatusCode.NotFound => "NotFound",
            HttpStatusCode.Conflict => "Conflict",
            HttpStatusCode.InternalServerError => "Internal",
            _ => statusCode.ToString()
        };

    private static string DefaultCodeFor(string type)
        => type switch
        {
            "BadRequest" => "BAD_REQUEST",
            "Unauthorized" => "UNAUTHORIZED",
            "Forbidden" => "FORBIDDEN",
            "NotFound" => "NOT_FOUND",
            "Validation" => "VALIDATION_ERROR",
            "Conflict" => "CONFLICT",
            "Internal" => "INTERNAL_ERROR",
            _ => type.ToUpperInvariant()
        };
}
