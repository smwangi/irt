namespace Irt.UnitTest.SharedKernel;

using System.Net;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class ResultErrorHandlingTests
{
    [Theory]
    [InlineData("BadRequest", "BAD_REQUEST", HttpStatusCode.BadRequest)]
    [InlineData("Unauthorized", "UNAUTHORIZED", HttpStatusCode.Unauthorized)]
    [InlineData("Forbidden", "FORBIDDEN", HttpStatusCode.Forbidden)]
    [InlineData("NotFound", "NOT_FOUND", HttpStatusCode.NotFound)]
    [InlineData("Conflict", "CONFLICT", HttpStatusCode.Conflict)]
    [InlineData("Internal", "INTERNAL_ERROR", HttpStatusCode.InternalServerError)]
    public void FactoryMethods_CreateExpectedProblemDetails(
        string errorType,
        string code,
        HttpStatusCode statusCode)
    {
        var error = errorType switch
        {
            "BadRequest" => IrtError.BadRequest("bad request"),
            "Unauthorized" => IrtError.Unauthorized("unauthorized"),
            "Forbidden" => IrtError.Forbidden("forbidden"),
            "NotFound" => IrtError.NotFound("not found"),
            "Conflict" => IrtError.Conflict("conflict"),
            _ => IrtError.Internal("internal")
        };

        var problem = error.ToProblemDetails();

        Assert.Equal((int)statusCode, problem.Status);
        Assert.Equal(code, problem.Title);
        Assert.Equal(errorType, problem.Extensions["errorType"]);
        Assert.Equal(code, problem.Extensions["code"]);
    }

    [Fact]
    public void ToProblemDetails_IncludesContextMetadata()
    {
        var context = new DefaultHttpContext
        {
            TraceIdentifier = "trace-123"
        };
        context.Request.Path = "/irt/test";

        var problem = IrtError.NotFound("missing").ToProblemDetails(context);

        Assert.Equal("/irt/test", problem.Instance);
        Assert.Equal("trace-123", problem.Extensions["traceId"]);
    }

    [Fact]
    public void ToActionResult_ReturnsProblemDetailsForFailure()
    {
        var result = Result.Failure<string>(IrtError.Conflict("duplicate"));

        var actionResult = Assert.IsAssignableFrom<ObjectResult>(result.ToActionResult());
        var problem = Assert.IsType<ProblemDetails>(actionResult.Value);

        Assert.Equal(StatusCodes.Status409Conflict, actionResult.StatusCode);
        Assert.Equal(StatusCodes.Status409Conflict, problem.Status);
        Assert.Equal("CONFLICT", problem.Title);
    }

    [Fact]
    public void ToActionResult_AddsContextMetadataWhenFormatted()
    {
        var context = new DefaultHttpContext
        {
            TraceIdentifier = "trace-456"
        };
        context.Request.Path = "/irt/failure";
        var actionContext = new ActionContext
        {
            HttpContext = context
        };
        var result = Assert.IsAssignableFrom<ObjectResult>(
            Result.Failure(IrtError.BadRequest("bad")).ToActionResult());

        result.OnFormatting(actionContext);
        var problem = Assert.IsType<ProblemDetails>(result.Value);

        Assert.Equal("/irt/failure", problem.Instance);
        Assert.Equal("trace-456", problem.Extensions["traceId"]);
    }
}
