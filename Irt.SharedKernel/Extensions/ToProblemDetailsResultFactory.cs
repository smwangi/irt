using Irt.SharedKernel.ErrorHandling.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Irt.SharedKernel.Extensions;

public static class ToProblemDetailsResultFactory
{
    public static ObjectResult ToProblemDetailsResult(this IrtError irtError)
    {
        var problem = new ProblemDetails
        {
            Title = irtError.Code,
            Detail = irtError.Message,
            Status = (int)irtError.StatusCode
        };
        
        foreach (var kv in irtError.Details)
        {
            problem.Extensions[kv.Key] = kv.Value;
        }

        return new ObjectResult(problem) { StatusCode = problem.Status };
    }
}