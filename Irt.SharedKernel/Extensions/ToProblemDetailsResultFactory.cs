using Irt.SharedKernel.ErrorHandling.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Irt.SharedKernel.Extensions;

public static class ToProblemDetailsResultFactory
{
    public static ObjectResult ToProblemDetailsResult(this Error error)
    {
        var problem = new ProblemDetails
        {
            Title = error.Code,
            Detail = error.Message,
            Status = (int)error.StatusCode
        };
        
        foreach (var kv in error.Details)
        {
            problem.Extensions[kv.Key] = kv.Value;
        }

        return new ObjectResult(problem) { StatusCode = problem.Status };
    }
}