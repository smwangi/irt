using System.Net;

namespace Irt.SharedKernel.ErrorHandling.Exceptions;

/// <summary>
/// Base class for all application thrown errors
/// </summary>
public class AppException(
    string message,
    HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
    string errorCode = "INTERNAL_ERROR",
    IDictionary<string, object>? details = null,
    bool isOperational = true,
    Exception? innerException = null)
    : Exception(message, innerException)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public string ErrorCode { get; } = errorCode;
    public IDictionary<string, object> Details { get; } = details ?? new Dictionary<string, object>();
    public bool IsOperational { get; } = isOperational;
}

public class BadRequestException(
    string message = "Bad Request",
    string errorCode = "BAD_REQUEST",
    IDictionary<string, object>? details = null,
    Exception? innerException = null)
    : AppException(message, HttpStatusCode.BadRequest, errorCode, details, true, innerException);

public sealed class UnauthorizedException(
    string message = "Unauthorized",
    string errorCode = "UNAUTHORIZED",
    IDictionary<string, object>? details = null,
    bool isOperational = true,
    Exception? innerException = null)
    : AppException(message, HttpStatusCode.Unauthorized, errorCode, details, isOperational, innerException);
    
    public sealed class ForbiddenException(
        string message = "Forbidden",
        string errorCode = "FORBIDDEN",
        IDictionary<string, object>? details = null,
        bool isOperational = true,
        Exception? innerException = null) 
        : AppException(message, HttpStatusCode.Forbidden, errorCode, details, isOperational, innerException);
        
    public class NotFoundException(
        string message = "Not Found",
        string errorCode = "NOT_FOUND",
        IDictionary<string, object>? details = null,
        bool isOperational = true,
        Exception? innerException = null) 
        : AppException(message, HttpStatusCode.NotFound, errorCode, details, isOperational, innerException);
        
    public sealed class TooManyRequestsException(
        string message = "Too Many Requests",
        string errorCode = "TOO_MANY_REQUESTS",
        IDictionary<string, object>? details = null,
        int? retryAfterSeconds = null,
        Exception? innerException = null) 
        : AppException(
            message, 
            HttpStatusCode.TooManyRequests, 
            errorCode, 
            BuildDetails(details, retryAfterSeconds),
            true, 
            innerException)
    {
        private static IDictionary<string, object>? BuildDetails(
            IDictionary<string, object>? details,
            int? retryAfterSeconds)
        {
            if (!retryAfterSeconds.HasValue)
            {
                return details;
            }

            var mergedDetails = details is null
                ? new Dictionary<string, object>()
                : new Dictionary<string, object>(details);

            mergedDetails["retryAfter"] = retryAfterSeconds.Value;
            return mergedDetails;
        }
    }

        
    public sealed class NotImplementedHttpException(
        string message = "Not Implemented",
        string errorCode = "NOT_IMPLEMENTED",
        IDictionary<string, object>? details = null,
        bool isOperational = true,
        Exception? innerException = null) 
        : AppException(message, HttpStatusCode.NotImplemented, errorCode, details, isOperational, innerException);
        
    public sealed class ServiceUnavailableException(
        string message = "Service Unavailable",
        string errorCode = "SERVICE_UNAVAILABLE",
        IDictionary<string, object>? details = null,
        bool isOperational = true,
        Exception? innerException = null) 
        : AppException(
            message, 
            HttpStatusCode.ServiceUnavailable, 
            errorCode, 
            details, 
            isOperational, 
            innerException);
        
// Domain Specific
    public sealed class ValidationException(
        IDictionary<string, string[]> validationErrors,
        Exception? innerException = null,
        string message = "Validation Failed",
        string errorCode = "VALIDATION_ERROR") 
        : BadRequestException(
            message,
            errorCode, 
            new Dictionary<string, object> { ["errors"] = validationErrors },
            innerException);
        
    public sealed class EntityNotFoundException(
        string entityName,
        object identifier,
        Exception? innerException = null) 
        : NotFoundException(
            $"{entityName} with identifier '{identifier}' not found",
            "ENTITY_NOT_FOUND", 
            new Dictionary<string, object>
            {
                ["entityName"] = entityName,
                ["identifier"] = identifier
            },
            true,
            innerException);
        
    public sealed class ConcurrencyException(
        string entityName,
        object identifier,
        Exception? innerException = null) 
        : AppException(
            $"Concurrency conflict for {entityName} '{identifier}'",
            HttpStatusCode.Conflict,
            "CONCURRENCY_CONFLICT", 
            new Dictionary<string, object>
            {
                ["entityName"] = entityName,
                ["identifier"] = identifier
            },
            true,
            innerException);
