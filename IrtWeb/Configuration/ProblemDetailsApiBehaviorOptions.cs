using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Results;
using Microsoft.AspNetCore.Mvc;

namespace IrtWeb.Configuration;

public static class ProblemDetailsApiBehaviorOptions
{
    public static void UseProblemDetailsValidationResponse(this ApiBehaviorOptions options)
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(item => item.Value?.Errors.Count > 0)
                .ToDictionary(
                    item => item.Key,
                    item => item.Value!.Errors
                        .Select(error => string.IsNullOrWhiteSpace(error.ErrorMessage)
                            ? "The input was not valid."
                            : error.ErrorMessage)
                        .ToArray());

            return IrtError.Validation(
                    "One or more validation errors occurred.",
                    details: new Dictionary<string, object>
                    {
                        ["errors"] = errors
                    })
                .ToProblemDetailsResult(context.HttpContext);
        };
    }
}
