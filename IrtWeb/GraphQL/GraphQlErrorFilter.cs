using HotChocolate;
using Irt.SharedKernel.ErrorHandling.Exceptions;

namespace IrtWeb.GraphQL;

public static class GraphQlErrorFilter
{
    public static IError OnError(IError error, bool includeExceptionDetails)
    {
        if (error.Exception?.GetBaseException() is AppException appException)
        {
            return ToGraphQlError(error, IrtError.FromException(appException));
        }

        return includeExceptionDetails && error.Exception is not null
            ? error.WithMessage(error.Exception.GetBaseException().Message + " | " + error.Exception.GetType().Name)
            : error;
    }

    private static IError ToGraphQlError(IError originalError, IrtError irtError)
    {
        var builder = ErrorBuilder
            .FromError(originalError)
            .SetMessage(irtError.Message)
            .SetCode(irtError.Code)
            .SetExtension("statusCode", (int)irtError.StatusCode)
            .SetExtension("errorType", irtError.Type);

        foreach (var detail in irtError.Details)
        {
            builder.SetExtension(detail.Key, detail.Value);
        }

        return builder.Build();
    }
}
