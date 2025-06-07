using Asp.Versioning;
using Irt.Application.Dispatchers;
using Irt.Application.ReportingScopes;
using Irt.Application.ReportingScopes.Commands;
using Irt.Application.ReportingScopes.Queries.Handlers;
using Irt.SharedKernel.Results;
using Microsoft.AspNetCore.Mvc;

namespace IrtWeb.ReportingScopes;

[ApiController]
[ControllerName("ReportingScope")]
[Route("api/v{version:apiVersion}")]
[ApiVersion("1.0")]
public class ReportingScopeController(
    ICommandDispatcher commandDispatcher,
    IQueryDispatcher queryDispatcher) : ControllerBase
{
    private const string ApiPrefix = "reporting-scope";
    [HttpGet(ApiPrefix)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReportingScopes()
    {
        var reportingScopes = await queryDispatcher
            .DispatchAsync<GetReportingScopeQuery, Result<List<ReportingScopeDto>>>(
                new GetReportingScopeQuery(), CancellationToken.None);
        
        return reportingScopes.IsSuccess
            ? Ok(reportingScopes)
            : NotFound(reportingScopes);
    }
    
    [HttpGet(ApiPrefix + "/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReportingScopeById([FromRoute] string id)
    {
        var reportingScope = await queryDispatcher
            .DispatchAsync<GetReportingScopeByIdQuery, Result<ReportingScopeDto>>(
                new GetReportingScopeByIdQuery(id), CancellationToken.None);
        
        return reportingScope.IsSuccess
            ? Ok(reportingScope)
            : NotFound(reportingScope);
    }
    
    [HttpPost(ApiPrefix)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateReportingScope([FromBody] CreateReportingScopeCommand command)
    {
        var response = await commandDispatcher
            .DispatchCommandAsync<CreateReportingScopeCommand, Result<ReportingScopeDto>>(
                command, CancellationToken.None);
        var r = response.IsSuccess;
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }
}