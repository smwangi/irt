using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Results;

namespace IrtWeb.GraphQL;

public static class GraphQlResultExtensions
{
    public static T ValueOrThrow<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return result.Value!;
        }

        throw result.ErrorOrThrow().ToAppException();
    }

    private static AppException ToAppException(this IrtError error)
        => new(error.Message, error.StatusCode, error.Code, error.Details);
}
