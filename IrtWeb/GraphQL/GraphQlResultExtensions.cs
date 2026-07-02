using Irt.SharedKernel.ErrorHandling.Exceptions;

namespace IrtWeb.GraphQL;

public static class GraphQlResultExtensions
{
    public static T ValueOrThrow<T>(this Irt.SharedKernel.Results.Result<T> result)
    {
        return result.IsSuccess
            ? result.Value!
            : throw result.ErrorOrThrow().ToAppException();
    }

    private static AppException ToAppException(this IrtError error)
        => new(error.Message, error.StatusCode, error.Code, error.Details);
}
