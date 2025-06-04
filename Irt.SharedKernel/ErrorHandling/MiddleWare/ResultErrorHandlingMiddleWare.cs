using System.Text.Json;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Extensions;
using Irt.SharedKernel.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Irt.SharedKernel.ErrorHandling.MiddleWare;

public class ResultErrorHandlingMiddleWare(
    RequestDelegate next,
    ILogger<ResultErrorHandlingMiddleWare> logger)
{
    private readonly RequestDelegate next = next;
    private readonly ILogger<ResultErrorHandlingMiddleWare> logger = logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (AppException ex)
        {
            logger.LogError(ex, "App Exception Occurred");
            var problem = new ProblemDetails
            {
                Title = ex.ErrorCode,
                Detail = ex.Message,
                Status = (int)ex.StatusCode,
                Type = $"https://httpstatuses.com/{(int)ex.StatusCode}"
            };
            context.Response.StatusCode = (int)ex.StatusCode;
            context.Response.ContentType = "application/problem+json";
            foreach (var kv in ex.Details)
            {
                problem.Extensions[kv.Key] = kv.Value;
            }
            var json = JsonSerializer.Serialize(problem, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await context.Response.WriteAsync(json);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unhandled Exception");
            var error = Error.Unexpected("An unexpected error occurred.");
            var problem = error.ToProblemDetailsResult();
            context.Response.StatusCode = problem.StatusCode ?? 500;
            var json = JsonSerializer.Serialize(problem.Value, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await context.Response.WriteAsync(json);
        }
    }
}