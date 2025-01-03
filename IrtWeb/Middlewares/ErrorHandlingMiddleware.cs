namespace IrtWeb.Middlewares
{
    using System.Net;
    using Irt.Application.Exceptions;
    using Microsoft.AspNetCore.Mvc;

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var (statusCode, message) = exception switch
            {
                ValidationException => (HttpStatusCode.BadRequest, exception.Message),
                NotFoundException => (HttpStatusCode.NotFound, "Resource Not Found"),
                _ => (HttpStatusCode.InternalServerError, "Internal Server Error")
            };

            var problemDetails = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = message,
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            context.Response.StatusCode = (int)statusCode;
            var response = new {error = message};
            return context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}