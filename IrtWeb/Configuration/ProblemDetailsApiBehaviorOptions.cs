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
            return IrtError.Validation(
                    "One or more validation errors occurred.",
                    details: ValidationErrorDetails.FromModelState(context.ModelState))
                .ToProblemDetailsResult(context.HttpContext);
        };
    }
}
