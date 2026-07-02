using Microsoft.AspNetCore.Mvc.ModelBinding;
using FluentValidationException = FluentValidation.ValidationException;

namespace IrtWeb.Configuration;

public static class ValidationErrorDetails
{
    public static Dictionary<string, object> FromModelState(ModelStateDictionary modelState)
        => new()
        {
            ["errors"] = modelState
                .Where(item => item.Value?.Errors.Count > 0)
                .ToDictionary(
                    item => item.Key,
                    item => item.Value!.Errors
                        .Select(error => string.IsNullOrWhiteSpace(error.ErrorMessage)
                            ? "The input was not valid."
                            : error.ErrorMessage)
                        .ToArray())
        };

    public static Dictionary<string, object> FromFluentValidation(FluentValidationException validationException)
        => new()
        {
            ["errors"] = validationException.Errors
                .GroupBy(error => string.IsNullOrWhiteSpace(error.PropertyName)
                    ? "request"
                    : error.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(error => error.ErrorMessage).ToArray())
        };
}
