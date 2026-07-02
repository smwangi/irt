using Irt.SharedKernel.ErrorHandling.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Irt.SharedKernel.Results;

public static class ResultExtension
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
        => result.IsSuccess
            ? new OkObjectResult(result.Value)
            : result.ErrorOrThrow().ToProblemDetailsResult();

    public static IActionResult ToActionResult(this Result result)
        => result.IsSuccess
            ? new OkResult()
            : result.ErrorOrThrow().ToProblemDetailsResult();

    public static ProblemDetails ToProblemDetails(this IrtError irtError)
    {
        var problem = new ProblemDetails
        {
            Title = irtError.Code,
            Detail = irtError.Message,
            Status = (int)irtError.StatusCode,
            Type = $"https://httpstatuses.com/{(int)irtError.StatusCode}"
        };

        problem.Extensions["code"] = irtError.Code;
        problem.Extensions["errorType"] = irtError.Type;

        foreach (var kv in irtError.Details)
        {
            problem.Extensions[kv.Key] = kv.Value;
        }

        return problem;
    }

    public static ProblemDetails ToProblemDetails(this IrtError irtError, HttpContext context)
    {
        var problem = irtError.ToProblemDetails();
        problem.Instance = context.Request.Path;
        problem.Extensions["traceId"] = context.TraceIdentifier;

        return problem;
    }

    public static ObjectResult ToProblemDetailsResult(this IrtError irtError)
        => new ProblemDetailsObjectResult(irtError);

    public static ObjectResult ToProblemDetailsResult(this IrtError irtError, HttpContext context)
        => new(irtError.ToProblemDetails(context)) { StatusCode = (int)irtError.StatusCode };

    private sealed class ProblemDetailsObjectResult : ObjectResult
    {
        private readonly IrtError error;

        public ProblemDetailsObjectResult(IrtError error)
            : base(error.ToProblemDetails())
        {
            this.error = error;
            StatusCode = (int)error.StatusCode;
        }

        public override void OnFormatting(ActionContext context)
        {
            Value = error.ToProblemDetails(context.HttpContext);
            StatusCode = (int)error.StatusCode;

            base.OnFormatting(context);
        }
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
