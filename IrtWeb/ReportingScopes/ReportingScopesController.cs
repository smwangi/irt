
using Irt.Application.Dispatchers;
using Irt.Application.ReportingScopes;
using Irt.Application.ReportingScopes.Commands;
using Irt.Application.ReportingScopes.Queries;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Results;
using Microsoft.AspNetCore.Mvc;
using IrtResultOfReportingScope = Irt.SharedKernel.Results.Result<Irt.Application.ReportingScopes.ReportingScopeDto>;


namespace IrtWeb.ReportingScopes;

[ApiController]
[Route("irt/v1/reporting-scopes")]
public class ReportingScopesController(
    ICommandDispatcher commandDispatcher,
    IQueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> List([FromQuery] string? search = null)
    {
        var result = await queryDispatcher
            .DispatchAsync<GetReportingScopeQuery, Irt.SharedKernel.Results.Result<List<ReportingScopeDto>>>(
                new GetReportingScopeQuery(search), CancellationToken.None);

        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }
    
    [HttpGet("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] string key)
    {
        var reportingScope = await queryDispatcher
            .DispatchAsync<GetReportingScopeByIdQuery, IrtResultOfReportingScope>(
                new GetReportingScopeByIdQuery(key), CancellationToken.None);
        return reportingScope.IsSuccess ? Ok(reportingScope.Value) : reportingScope.ToActionResult();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateReportingScope([FromBody] CreateReportingScopeRequest request)
    {
        var command = request.ToCommand();
        var response = await commandDispatcher
            .DispatchCommandAsync<CreateReportingScopeCommand, IrtResultOfReportingScope>(
                command, CancellationToken.None);

        return response.Match(
            onSuccess: dto => CreatedAtAction(
                nameof(Get),
                new { key = dto.Id },
                dto),
            onFailure: _ => response.ToActionResult());
    }

    [HttpPut("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateReportingScope(
        [FromRoute] string? key,
        [FromBody] UpdateReportingScopeRequest request)
    {
        if (key is null)
        {
            return Irt.SharedKernel.Results.Result
                .Failure(IrtError.BadRequest("Key is required."))
                .ToActionResult();
        }
        
        var command = request.ToCommand(key);
        var result = await commandDispatcher
            .DispatchCommandAsync<UpdateReportingScopeCommand, IrtResultOfReportingScope>(
                command, CancellationToken.None);
        return result.ToActionResult();
    }
    
    [HttpPatch("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PatchReportingScope(
        [FromRoute] string? key,
        [FromBody] PatchReportingScopeRequest request)
    {
        if (key is null)
        {
            return Irt.SharedKernel.Results.Result
                .Failure(IrtError.BadRequest("Key is required."))
                .ToActionResult();
        }
        
        var command = request.ToCommand(key);
        var result = await commandDispatcher
            .DispatchCommandAsync<PatchReportingScopeCommand, IrtResultOfReportingScope>(
                command, CancellationToken.None);
        return result.ToActionResult();
    }
}
