
using Irt.SharedKernel.ErrorHandling.Exceptions;

namespace Irt.SharedKernel.Results;

public static class ResultExtension
{
    // Bind (Monadic chaining)
    public static Result<TOut> Bind<T, TOut>(
        this Result<T> result,
        Func<T, Result<TOut>> bind)
    {
        return result.IsSuccess
            ? bind(result.Value!)
            : Result<TOut>.Failure(result.Error!);
    } 
    
    // BindAsync (async chaining)
    public static async Task<Result<TOut>> BindAsync<T, TOut>(
        this Result<T> result,
        Func<T, Task<Result<TOut>>> bind)
    {
        return result.IsSuccess
            ? await bind(result.Value!)
            : Result<TOut>.Failure(result.Error!);
    }
    
    public static async Task<Result<TOut>> BindAsync<T, TOut>(
        this Task<Result<T>> task,
        Func<T, Task<Result<TOut>>> bind)
    {
        var result = await task;
        return result.IsSuccess
            ? await bind(result.Value!)
            : Result<TOut>.Failure(result.Error!);
    }
    
    // Map (Monadic mapping transform value, keep same metadata)
    public static Result<TOut> Map<T, TOut>(
        this Result<T> result,
        Func<T, TOut> map)
    {
        return result.IsSuccess
            ? Result<TOut>.Success(map(result.Value!), result.Metadata)
            : Result<TOut>.Failure(result.Error!, result.Metadata);
    }
    
    // Map Async (async mapping transform value, keep same metadata)
    public static async Task<Result<TOut>> MapAsync<T, TOut>(
        this Result<T> result,
        Func<T, Task<TOut>> map)
    {
        return result.IsSuccess
            ? Result<TOut>.Success(await map(result.Value!), result.Metadata)
            : Result<TOut>.Failure(result.Error!, result.Metadata);
    }
    
    public static async Task<Result<TOut>> MapAsync<T, TOut>(
        this Task<Result<T>> resultTask,
        Func<T, TOut> map)
    {
        var result = await resultTask;
        return result.IsSuccess
            ? Result<TOut>.Success(map(result.Value!), result.Metadata)
            : Result<TOut>.Failure(result.Error!, result.Metadata);
    }
    
    // Match 
    public static TResult Match<T, TResult>(
        this Result<T> result,
        Func<T, TResult> onSuccess,
        Func<Error, TResult> onFailure)
    {
        return result.IsSuccess
            ? onSuccess(result.Value!)
            : onFailure(result.Error!);
    }
    
    // MatchAsync
    public static async Task<TResult> MatchAsync<T, TResult>(
        this Result<T> result,
        Func<T, Task<TResult>> onSuccess,
        Func<Error, Task<TResult>> onFailure)
    {
        return result.IsSuccess
            ? await onSuccess(result.Value!)
            : await onFailure(result.Error!);
    }
    
    // Tap (Side effect on success)
    public static Result<T> Tap<T>(
        this Result<T> result,
        Action<T> action)
    {
        if (result.IsSuccess)
        {
            action(result.Value!);
        }

        return result;
    }
    
    // TapAsync (async side effect on success)
    public static async Task<Result<T>> TapAsync<T>(
        this Result<T> result,
        Func<T, Task> action)
    {
        if (result.IsSuccess)
        {
            await action(result.Value!);
        }

        return result;
    }
    
    // Ensure validation within the chain
    public static Result<T> Ensure<T>(
        this Result<T> result,
        Func<T, bool> predicate,
        Error errorIfFalse)
    {
        if (result.IsFailure)
        {
            return result;
        }

        return predicate(result.Value!)
            ? result
            : Result<T>.Failure(errorIfFalse, result.Metadata);
    }
    
    public static async Task<Result<T>> EnsureAsync<T>(
        this Task<Result<T>> task,
        Func<T, bool> predicate,
        Error error)
    {
        var result = await task;
        return 
            result.IsFailure 
                ? result 
                : predicate(result.Value!) ? result : Result<T>.Failure(error);
    }
    
    // WithMetadata (add metadata to the result)
    public static Result<T> WithMetadata<T>(
        this Result<T> result,
        Dictionary<string, object> metadata)
    {
        return result.IsFailure 
            ? result 
            : Result<T>.Success(result.Value!, metadata);
    }
    
    // Combine multiple results
    public static Result<IReadOnlyList<T>> Combine<T>(params Result<T>[] results)
    {
        var list = new List<T>();
        foreach (var result in results)
        {
            if (result.IsFailure)
            {
                return Result<IReadOnlyList<T>>.Failure(result.Error!, result.Metadata);
            }

            if (result.Value != null) list.Add(result.Value);
        }

        return Result<IReadOnlyList<T>>.Success(list);
    }
    
    public static async Task<Result<T>> ToResult<T>(
        this Task<T?> task,
        Error notFoundError)
        where T : class
    {
        var result = await task;
        return result is null 
            ? Result<T>.Failure(notFoundError) 
            : Result<T>.Success(result);
    }
    
    public static async Task<Result<T>> ToResult<T>(
        this ValueTask<T> task,
        Error notFoundError) where T : class?
    {
        var result = await task;
        return result is null
            ? Result<T>.Failure(notFoundError)
            : Result<T>.Success(result);
    }

    public static async Task<Result<List<T>>> ToResult<T>(
        this Task<List<T>> task,
        Error notFoundError,
        bool treatEmptyListAsSuccess = true,
        bool includeCountMetadata = false)
    {
        var result = await task;
        if (result is null)
        {
            return Result<List<T>>.Failure(notFoundError);
        }

        var metadata = includeCountMetadata
            ? new Dictionary<string, object> { ["TotalCount"] = result.Count }
            : null;
        
        return Result<List<T>>.Success(result, metadata);
    }

    public static async Task<Result<List<T>>> ToResult<T>(
        this Task<List<T>> task,
        Error notFoundError,
        bool includeCountMetadata = false)
    {
        var result = await task;

        var metadata = includeCountMetadata
            ? new Dictionary<string, object> { ["TotalCount"] = result.Count }
            : null;
        
        return Result<List<T>>.Success(result, metadata);
    }
}