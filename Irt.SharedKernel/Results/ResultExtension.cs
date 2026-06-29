
using Irt.SharedKernel.Common;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Irt.SharedKernel.Results;

public static class ResultExtension
{
    // Convert Result<T> to IActionResult
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }
        
        // Map IrtError to appropriate HTTP status codes
        return result.IrtError switch
        {
            { Type: "NotFound" } error => new NotFoundObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            }),
            { Type: "Validation" } error => new BadRequestObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            }),
            { Type: "BadRequest" } error => new BadRequestObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            }),
            { Type: "Unauthorized" } error => new UnauthorizedObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            }),
            { Type: "Forbidden" } error => new ObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            }) { StatusCode = 403 },
            { Type: "Conflict" } error => new ConflictObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            }),
            { Type: "Internal" } error => new ObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            }) { StatusCode = 500 },
            { } error => new BadRequestObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            })
        };
    }
    
    // Convert non-generic Result to IActionResult
    public static IActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
        {
            return new OkResult();
        }
        
        return result.IrtError switch
        {
            { Type: "NotFound" } error => new NotFoundObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            }),
            { Type: "Validation" } error => new BadRequestObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            }),
            { Type: "BadRequest" } error => new BadRequestObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            }),
            { Type: "Unauthorized" } error => new UnauthorizedObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            }),
            { Type: "Forbidden" } error => new ObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            }) { StatusCode = 403 },
            { Type: "Conflict" } error => new ConflictObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            }),
            { Type: "Internal" } error => new ObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            }) { StatusCode = 500 },
            { } error => new BadRequestObjectResult(new { 
                error = error.Message, 
                code = error.Code,
                details = error.Details 
            })
        };
    }
    
    // For OData specifically - returns the value directly or throws
    public static T ToODataResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return result.Value;
            
        throw new InvalidOperationException($"OData query failed: {result.IrtError?.Message}");
    }
    
    // Combine multiple results
    public static Result Combine(params Result[] results)
    {
        var failures = results.Where(r => r.IsFailure).ToArray();
        
        if (failures.Length == 0)
            return Result.Success();
        
        // Combine all error messages
        var combinedMessage = string.Join("; ", failures.Select(f => f.IrtError!.Message));
        var combinedError = IrtError.BadRequest($"Multiple errors occurred: {combinedMessage}");
        
        return Result.Failure(combinedError);
    }
    
    public static Result<T[]> Combine<T>(params Result<T>[] results)
    {
        var failures = results.Where(r => r.IsFailure).ToArray();
        
        if (failures.Length == 0)
            return Result<T[]>.Success(results.Select(r => r.Value!).ToArray());
        
        // Combine all error messages
        var combinedMessage = string.Join("; ", failures.Select(f => f.IrtError!.Message));
        var combinedError = IrtError.BadRequest($"Multiple errors occurred: {combinedMessage}");
        
        return Result<T[]>.Failure(combinedError);
    }
    
    // Execute action on success
    public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccess)
            action(result.Value!);
        
        return result;
    }
    
    // Execute action on failure
    public static Result<T> OnFailure<T>(this Result<T> result, Action<IrtError> action)
    {
        if (result.IsFailure)
            action(result.IrtError!);
        
        return result;
    }
    
    // Convert to nullable
    public static T? ToNullable<T>(this Result<T> result) where T : struct
    {
        return result.IsSuccess ? result.Value : null;
    }
    
    // Ensure (add validation)
    public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, IrtError error)
    {
        if (result.IsFailure)
            return result;
        
        return predicate(result.Value!) 
            ? result 
            : Result<T>.Failure(error);
    }
    
    // Async ensure
    public static async Task<Result<T>> EnsureAsync<T>(this Result<T> result, Func<T, Task<bool>> predicate, IrtError error)
    {
        if (result.IsFailure)
            return result;
        
        return await predicate(result.Value!) 
            ? result 
            : Result<T>.Failure(error);
    }
}