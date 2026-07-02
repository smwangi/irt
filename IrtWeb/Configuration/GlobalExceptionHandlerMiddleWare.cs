using System.Text.Json;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Results;
using Microsoft.AspNetCore.Mvc;
using FluentValidationException = FluentValidation.ValidationException;

namespace IrtWeb.Configuration;

public class GlobalExceptionHandlerMiddleWare(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlerMiddleWare> logger)
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (FluentValidationException validationException)
        {
            logger.LogWarning(validationException, "Validation Exception");
            await WriteErrorAsync(
                context,
                IrtError.Validation(
                    "One or more validation errors occurred.",
                    details: ValidationErrorDetails.FromFluentValidation(validationException)));
        }
        catch (AppException appException)
        {
            logger.LogWarning(appException, "Handled Exception");
            AddRetryAfterIfPresent(context, ex: appException);
            await WriteErrorAsync(context, IrtError.FromException(appException));
        }
        catch (ArgumentException argumentException)
        {
            logger.LogWarning(argumentException, "Bad Request");
            await WriteErrorAsync(context, IrtError.BadRequest(argumentException.Message));
        }
        catch (UnauthorizedAccessException unauthorizedAccessException)
        {
            logger.LogWarning(unauthorizedAccessException, "Unauthorized");
            await WriteErrorAsync(context, IrtError.Unauthorized("Unauthorized."));
        }
        catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
        {
            logger.LogInformation("Request was canceled by the client.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await WriteErrorAsync(context, IrtError.Unexpected());
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

    private static Task WriteProblemAsync(HttpContext context, ProblemDetails problem)
    {
        var json = JsonSerializer.Serialize(problem, JsonOptions);
        return context.Response.WriteAsync(json);
    }

    private Task WriteErrorAsync(HttpContext context, IrtError error)
    {
        if (context.Response.HasStarted)
        {
            logger.LogWarning(
                "Could not write {ErrorCode} response because the HTTP response has already started.",
                error.Code);
            return Task.CompletedTask;
        }

        context.Response.Clear();
        context.Response.StatusCode = (int)error.StatusCode;
        context.Response.ContentType = "application/problem+json";

        return WriteProblemAsync(context, error.ToProblemDetails(context));
    }
}
