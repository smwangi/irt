using Irt.SharedKernel.ErrorHandling.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Irt.SharedKernel.Results;

public static class ResultExtension
{
    extension<T>(Result<T> result)
    {
        public IActionResult ToActionResult()
            => result.IsSuccess
                ? new OkObjectResult(result.Value)
                : result.ErrorOrThrow().ToProblemDetailsResult();

        // Execute action on success
        public Result<T> OnSuccess(Action<T> action)
        {
            if (result.IsSuccess)
                action(result.Value!);
            
            return result;
        }
        
        // Execute action on failure
        public Result<T> OnFailure(Action<IrtError> action)
        {
            if (result.IsFailure)
                action(result.IrtError!);
            
            return result;
        }
        // Ensure (add validation)
        public Result<T> Ensure(Func<T, bool> predicate, IrtError error)
        {
            if (result.IsFailure)
                return result;
            
            return predicate(result.Value!) 
                ? result 
                : Result<T>.Failure(error);
        }
        
        // Async ensure
        public async Task<Result<T>> EnsureAsync(Func<T, Task<bool>> predicate, IrtError error)
        {
            if (result.IsFailure)
                return result;
            
            return await predicate(result.Value!) 
                ? result 
                : Result<T>.Failure(error);
        }
    }

    extension<T>(Result<T> result) where T : struct
    {
        // Convert to nullable
        public T? ToNullable()
        {
            return result.IsSuccess ? result.Value : null;
        }
    }

    extension(Result result)
    {
        public IActionResult ToActionResult()
            => result.IsSuccess
                ? new OkResult()
                : result.ErrorOrThrow().ToProblemDetailsResult();
    }

    extension(IrtError irtError)
    {
        public ProblemDetails ToProblemDetails()
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

        public ProblemDetails ToProblemDetails(HttpContext context)
        {
            var problem = irtError.ToProblemDetails();
            problem.Instance = context.Request.Path;
            problem.Extensions["traceId"] = context.TraceIdentifier;

            return problem;
        }

        public ObjectResult ToProblemDetailsResult()
            => new ProblemDetailsObjectResult(irtError);

        public ObjectResult ToProblemDetailsResult(HttpContext context)
            => new(irtError.ToProblemDetails(context)) { StatusCode = (int)irtError.StatusCode };
    }

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
}
