using Irt.SharedKernel.ErrorHandling.Exceptions;

namespace Irt.SharedKernel.Results;

public readonly struct Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public Error? Error { get; }
    
    public Dictionary<string, object> Metadata { get; }
    
    private Result(T? value)
    {
        IsSuccess = true;
        Value = value;
        Error = null;
    }
    
    private Result(T? value, Dictionary<string, object>? metadata = null)
    {
        IsSuccess = true;
        Value = value;
        Error = null;
        Metadata = metadata ?? new ();
    }

    private Result(Error error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }
    
    private Result(Error error, Dictionary<string, object>? metadata = null)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
        Metadata = metadata ?? new ();
    }
    
    public static Result<T> Ok (T value) => new(value);

    public static Result<T> Ok(T value, Dictionary<string, object>? metadata = null)
        => new(value: value, metadata);

    public static Result<T> Fail(Error error) => new(error);
    public static Result<T> Fail(Error error, Dictionary<string, object>? metadata = null)
        => new(error: error, metadata);
    
    // Monadic Combinators
    public Result<TOut> Map<TOut>(Func<T, TOut> map)
    {
        return IsSuccess
            ? Result<TOut>.Ok(map(Value!))
            : Result<TOut>.Fail(Error!);
    }

    public Result<U> Bind<U>(Func<T, Result<U>> bind)
    {
        return IsSuccess
            ? bind(Value!)
            : Result<U>.Fail(Error!);
    }
    
    public async Task<Result<U>> BindAsyn<U>(Func<T, Task<Result<U>>> bind)
    {
        return IsSuccess
            ? await bind(Value!)
            : Result<U>.Fail(Error!, Metadata);
    }

    public Result<T> Tap(Action<T> action)
    {
        if (IsSuccess)
        {
            action(Value!);
        }

        return this;
    }
    
    public T Unwrap() 
        => IsSuccess ? Value! : throw new InvalidOperationException("Result is a failure");
    
    public TMeta? GetMetadata<TMeta>(string key, TMeta? defaultValue = default)
    {
        if (Metadata.TryGetValue(key, out var value))
        {
            return (TMeta?)value;
        }

        return defaultValue;
    }
    
    public Result<T> WithMetadata(string key, object value)
    {
        var newMetadata = new Dictionary<string, object>(Metadata) { [key] = value };
        return IsSuccess ? Result<T>.Ok(Value!, newMetadata) : Result<T>.Fail(Error!, newMetadata);
    }
}

// Void Scenarios
public sealed class Result
{ 
    public bool IsSuccess { get; }
    public Error? Error { get; }
    private Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Ok()
        => new(true, error: null);
    public static Result Fail(Error error) => new(false, error);
}
