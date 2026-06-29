using System.Diagnostics.CodeAnalysis;
using Irt.SharedKernel.ErrorHandling.Exceptions;

namespace Irt.SharedKernel.Results;

public readonly struct Result<T>
{
    [MemberNotNullWhen(returnValue: true, nameof(Value))]
    public bool IsSuccess => !IsFailure;
    
    [MemberNotNullWhen(true, nameof(IrtError))]
    public bool IsFailure { get; }

    public T? Value { get; }
    public IrtError? IrtError { get; }
    
    public IrtError ErrorOrThrow()
        => IrtError ?? throw new InvalidOperationException("Expected IrtError to be present.");
    
    public Dictionary<string, object> Metadata { get; }

    // Success constructor
    private Result(T value, Dictionary<string, object>? metadata = null)
    {
        IsFailure = false;
        Value = value;
        IrtError = null;
        Metadata = metadata ?? new();
    }

    // Failure constructor
    private Result(IrtError irtError, Dictionary<string, object>? metadata = null)
    {
        IsFailure = true;
        Value = default;
        IrtError = irtError;
        Metadata = metadata ?? new();
    }

    // Factory methods
    public static Result<T> Success(T value, Dictionary<string, object>? metadata = null)
        => new(value, metadata);

    public static Result<T> Failure(IrtError irtError, Dictionary<string, object>? metadata = null)
        => new(irtError, metadata);

    // Combinators
    public Result<U> Map<U>(Func<T, U> map)
        => IsSuccess
            ? Result<U>.Success(map(Value!), Metadata)
            : Result<U>.Failure(IrtError!, Metadata);

    public async Task<Result<U>> MapAsync<U>(Func<T, Task<U>> mapAsync)
        => IsSuccess
            ? Result<U>.Success(await mapAsync(Value!), Metadata)
            : Result<U>.Failure(IrtError!, Metadata);

    public Result<U> Bind<U>(Func<T, Result<U>> bind)
        => IsSuccess
            ? bind(Value!)
            : Result<U>.Failure(IrtError!, Metadata);

    public async Task<Result<U>> BindAsync<U>(Func<T, Task<Result<U>>> bind)
        => IsSuccess
            ? await bind(Value!)
            : Result<U>.Failure(IrtError!, Metadata);

    public Result<T> Tap(Action<T> action)
    {
        if (IsSuccess)
            action(Value!);

        return this;
    }
    
    public async Task<Result<T>> TapAsync(Func<T, Task> actionAsync)
    {
        if (IsSuccess)
            await actionAsync(Value!);

        return this;
    }

    public T Unwrap()
        => IsSuccess ? Value! : throw new InvalidOperationException("Result is a failure.");

    public TMeta? GetMetadata<TMeta>(string key, TMeta? defaultValue = default)
    {
        if (Metadata.TryGetValue(key, out var value) && value is TMeta casted)
            return casted;

        return defaultValue;
    }

    public Result<T> WithMetadata(string key, object value)
    {
        var newMetadata = new Dictionary<string, object>(Metadata) { [key] = value };
        return IsSuccess
            ? Success(Value!, newMetadata)
            : Failure(IrtError!, newMetadata);
    }

    public bool TryGetMetadata<TMeta>(string key, out TMeta? value)
    {
        if (Metadata.TryGetValue(key, out var raw) && raw is TMeta casted)
        {
            value = casted;
            return true;
        }

        value = default;
        return false;
    }
    
    // Pattern matching
    public TOut Match<TOut>(Func<T, TOut> onSuccess, Func<IrtError, TOut> onFailure)
        => IsSuccess ? onSuccess(Value!) : onFailure(IrtError!);
    
    public async Task<TOut> MatchAsync<TOut>(Func<T, Task<TOut>> onSuccess, Func<IrtError, Task<TOut>> onFailure)
        => IsSuccess ? await onSuccess(Value!) : await onFailure(IrtError!);
    
    // Try get value without throwing exception
    public bool TryGetValue([NotNullWhen(true)] out T? value)
    {
        value = IsSuccess ? Value : default;
        return IsSuccess;
    }
    
    // Implicit conversion from T to Result<T>
    public static implicit operator Result<T>(T value) => Success(value);
}

public readonly struct Result
{
    [MemberNotNullWhen(true, nameof(IrtError))]
    public bool IsFailure { get; }
    
    [MemberNotNullWhen(returnValue: false, nameof(IrtError))]
    public bool IsSuccess => !IsFailure;
    
    public IrtError? IrtError { get; }
    public Dictionary<string, object> Metadata { get; }
    
    public IrtError ErrorOrThrow()
        => IrtError ?? throw new InvalidOperationException("Expected IrtError to be present.");
    
    // Success constructor
    private Result(Dictionary<string, object>? metadata = null)
    {
        IsFailure = false;
        IrtError = null;
        Metadata = metadata ?? new();
    }
    
    // Failure constructor
    private Result(IrtError irtError, Dictionary<string, object>? metadata = null)
    {
        IsFailure = true;
        IrtError = irtError;
        Metadata = metadata ?? new();
    }
    
    // Factory methods
    public static Result Success(Dictionary<string, object>? metadata = null)
        => new(metadata);
        
    public static Result Failure(IrtError irtError, Dictionary<string, object>? metadata = null)
        => new(irtError, metadata);
        
    // Generic factory methods
    public static Result<T> Success<T>(T value, Dictionary<string, object>? metadata = null)
        => Result<T>.Success(value, metadata);
        
    public static Result<T> Failure<T>(IrtError irtError, Dictionary<string, object>? metadata = null)
        => Result<T>.Failure(irtError, metadata);
        
    // Combinators
    public Result Tap(Action action)
    {
        if (IsSuccess)
            action();

        return this;
    }
    
    public async Task<Result> TapAsync(Func<Task> actionAsync)
    {
        if (IsSuccess)
            await actionAsync();

        return this;
    }
    
    public TMeta? GetMetadata<TMeta>(string key, TMeta? defaultValue = default)
    {
        if (Metadata.TryGetValue(key, out var value) && value is TMeta casted)
            return casted;

        return defaultValue;
    }

    public Result WithMetadata(string key, object value)
    {
        var newMetadata = new Dictionary<string, object>(Metadata) { [key] = value };
        return IsSuccess
            ? Success(newMetadata)
            : Failure(IrtError!, newMetadata);
    }

    public bool TryGetMetadata<TMeta>(string key, out TMeta? value)
    {
        if (Metadata.TryGetValue(key, out var raw) && raw is TMeta casted)
        {
            value = casted;
            return true;
        }

        value = default;
        return false;
    }
}
