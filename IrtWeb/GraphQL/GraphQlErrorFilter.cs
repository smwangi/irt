using HotChocolate;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using IrtWeb.Configuration;
using FluentValidationException = FluentValidation.ValidationException;

namespace IrtWeb.GraphQL;

public static class GraphQlErrorFilter
{
    public static IError OnError(IError error, bool includeExceptionDetails)
    {
        var baseException = error.Exception?.GetBaseException();

        if (baseException is AppException appException)
        {
            return ToGraphQlError(error, IrtError.FromException(appException));
        }

        if (baseException is FluentValidationException validationException)
        {
            return ToValidationError(error, validationException);
        }

        return includeExceptionDetails && error.Exception is not null
            ? error.WithMessage(error.Exception.GetBaseException().Message + " | " + error.Exception.GetType().Name)
            : error;
    }

    private static IError ToValidationError(IError originalError, FluentValidationException validationException)
    {
        var builder = ErrorBuilder
            .FromError(originalError)
            .SetMessage("One or more validation errors occurred.")
            .SetCode("VALIDATION_ERROR")
            .SetExtension("statusCode", 400)
            .SetExtension("errorType", "Validation")
            .SetExtension("errors", ValidationErrorDetails.FromFluentValidation(validationException)["errors"]);

        return builder.Build();
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
