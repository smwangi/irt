using Irt.SharedKernel.ErrorHandling.Exceptions;

namespace Irt.SharedKernel.Results;

public readonly struct Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public T? Value { get; }
    public IrtError? IrtError { get; }
    public Dictionary<string, object> Metadata { get; }

    // Success constructor
    private Result(T value, Dictionary<string, object>? metadata = null)
    {
        IsSuccess = true;
        Value = value;
        IrtError = null;
        Metadata = metadata ?? new();
    }

    // Failure constructor
    private Result(IrtError irtError, Dictionary<string, object>? metadata = null)
    {
        IsSuccess = false;
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
}
