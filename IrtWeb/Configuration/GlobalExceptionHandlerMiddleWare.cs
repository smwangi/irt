using System.Net;
using System.Text.Json;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace IrtWeb.Configuration;

public class GlobalExceptionHandlerMiddleWare(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlerMiddleWare> logger)
{
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (AppException appException)
        {
            logger.LogWarning(appException, "Handled Exception");
            context.Response.StatusCode = (int)appException.StatusCode;
            context.Response.ContentType = "application/problem+json";
    
            AddRetryAfterIfPresent(context, ex: appException);
            var problem = new ProblemDetails
            {
                Title = appException.ErrorCode,
                Detail = appException.Message,
                Status = (int)appException.StatusCode,
                Type = $"https://httpstatuses.com/{(int)appException.StatusCode}"
            };

            foreach (var kv in appException.Details)
            {
                problem.Extensions[kv.Key] = kv.Value;
            }

            var json = JsonSerializer.Serialize(problem, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/problem+json";
            var problem = new ProblemDetails
            {
                Title = "INTERNAL_ERROR",
                Detail = "An unexpected error occurred.",
                Status = 500,
                Type = "https://httpstatuses.com/500"
            };

            var json = JsonSerializer.Serialize(problem, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }

    private static void AddRetryAfterIfPresent(HttpContext context, AppException ex)
    {
        if (ex is TooManyRequestsException || ex is ServiceUnavailableException)
        {
            if (ex.Details.TryGetValue("retryAfter", out var retryAfter) && retryAfter is int retrySeconds)
            {
                context.Response.Headers["Retry-After"] = retrySeconds.ToString();
            }
        }
    }
}