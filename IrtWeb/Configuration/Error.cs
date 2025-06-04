using System.Net;

namespace IrtWeb.Configuration;

public class Error
{
    private Error(
        string code, 
        string message, 
        HttpStatusCode statusCode, 
        Dictionary<string, object>? details = null)
    {
        Code = code;
        Message = message;
        StatusCode = statusCode;
        Details = details ?? new Dictionary<string, object>();
    }
    public string Message { get; }
    public string Code { get; }
    public HttpStatusCode  StatusCode { get; }
    public Dictionary<string, object> Details { get; }
    
    public static readonly Error None = new Error("", "", HttpStatusCode.OK);
    public static Error BadRequest(
        string message = "Bad Request", 
        string code = "BadRequest", 
        Dictionary<string, object>? details = null)
    {
        return new Error(code, message, HttpStatusCode.BadRequest, details);
    }
    
    public static Error UnAuthorized(
        string message = "Unauthorized", 
        string code = "UnAuthorized", 
        Dictionary<string, object>? details = null)
    {
        return new Error(code, message, HttpStatusCode.Unauthorized, details);
    }
    
    public static Error NotFound(
        string message = "Resource Not Found", 
        string code = "NotFound", 
        Dictionary<string, object>? details = null)
    {
        return new Error(code, message, HttpStatusCode.NotFound, details);
    }
    public static Error Forbidden(
        string message = "Forbidden", 
        string code = "Forbidden", 
        Dictionary<string, object>? details = null)
    {
        return new Error(code, message, HttpStatusCode.Forbidden, details);
    }
    public static Error InternalServerError(
        string message = "Internal Server Error", 
        string code = "InternalServerError", 
        Dictionary<string, object>? details = null)
    {
        return new Error(code, message, HttpStatusCode.InternalServerError, details);
    }
    public static Error Conflict(string message = "Conflict", string code = "Conflict", Dictionary<string, object>? details = null)
    {
        return new Error(code, message, HttpStatusCode.Conflict, details);
    }
    public static Error NotAcceptable(
        string message = "Not Acceptable", 
        string code = "NotAcceptable", 
        Dictionary<string, object>? details = null)
    {
        return new Error(code, message, HttpStatusCode.NotAcceptable, details);
    }
    public static Error PreconditionFailed(
        string message = "Precondition Failed", 
        string code = "PreconditionFailed", 
        Dictionary<string, object>? details = null)
    {
        return new Error(code, message, HttpStatusCode.PreconditionFailed, details);
    }
    public static Error TooManyRequests(
        string message = "Too Many Requests", 
        string code = "TooManyRequests", 
        Dictionary<string, object>? details = null)
    {
        return new Error(code, message, HttpStatusCode.TooManyRequests, details);
    }
    public static Error ServiceUnavailable(
        string message = "Service Unavailable", 
        string code = "ServiceUnavailable", 
        Dictionary<string, object>? details = null)
    {
        return new Error(code, message, HttpStatusCode.ServiceUnavailable, details);
    }
    public static Error BadGateway(
        string message = "Bad Gateway", 
        string code = "BadGateway", 
        Dictionary<string, object>? details = null)
    {
        return new Error(code, message, HttpStatusCode.BadGateway, details);
    }
    public static Error GatewayTimeout(
        string message = "Gateway Timeout", 
        string code = "GatewayTimeout", 
        Dictionary<string, object>? details = null)
    {
        return new Error(code, message, HttpStatusCode.GatewayTimeout, details);
    }
    public static Error NotImplemented(
        string message = "Not Implemented", 
        string code = "NotImplemented", 
        Dictionary<string, object>? details = null)
    {
        return new Error(code, message, HttpStatusCode.NotImplemented, details);
    }
    public static Error UnprocessableEntity(
        string message = "Unprocessable Entity", 
        string code = "UnprocessableEntity", 
        Dictionary<string, object>? details = null)
    {
        return new Error(code, message, HttpStatusCode.UnprocessableEntity, details);
    }
    
    public static Error CommandHandlerNotFound(string commandName)
        => NotImplemented(
            $"No handler registered for command name {commandName}",
            "CommandHandlerNotFound",
            new Dictionary<string, object>
            {
                { "CommandName", commandName }
            });
    
    public static Error QueryHandlerNotFound(string queryName)
        => NotImplemented(
            $"No handler registered for query name {queryName}",
            "QueryHandlerNotFound",
            new Dictionary<string, object>
            {
                { "QueryName", queryName }
            });
}