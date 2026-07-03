using Microsoft.AspNetCore.Http;

namespace Irt.Application.Configuration.ContextAccessor;

public class OperationContextAccessor(IHttpContextAccessor contextAccessor) : IOperationContextAccessor
{
    public string OperationType
    {
        get
        {
            var method = contextAccessor.HttpContext?.Request.Method;
            return method switch
            {
                "GET" => "Read",
                "POST" => "Create",
                "PUT" or "PATCH" => "Update",
                "DELETE" => "Delete",
                _ => throw new NotImplementedException($"Operation type for method {method} is not implemented")
            };
        }
    }
}

public interface IOperationContextAccessor
{
    string OperationType { get; }
}