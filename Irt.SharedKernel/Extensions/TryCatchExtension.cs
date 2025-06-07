using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Results;

namespace Irt.SharedKernel.Extensions;

public static class TryCatchExtension
{
    public static Result<T> Try<T>(
        Func<T> action,
        Func<Exception, IrtError>? errorHandler = null)
    {
        try
        {
            return Result<T>.Success(action());
        }
        catch (Exception e)
        {
            var error = errorHandler?.Invoke(e) ?? IrtError.Unexpected(e.Message);
            return Result<T>.Failure(error);
        }
    }

    public static async Task<Result<T>> TryAsync<T>(
        Func<Task<T>> action,
        Func<Exception, IrtError>? errorHandler = null)
    {
        try
        {
            var value = await action();
            return Result<T>.Success(value);
        }
        catch (Exception e)
        {
            var error = errorHandler?.Invoke(e) ?? IrtError.Unexpected(e.Message);
            return Result<T>.Failure(error);
        }
    }
}