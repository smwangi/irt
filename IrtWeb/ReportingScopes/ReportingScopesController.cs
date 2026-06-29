
using Irt.Application.Dispatchers;
using Irt.Application.ReportingScopes;
using Irt.Application.ReportingScopes.Commands;
using Irt.Application.ReportingScopes.Queries;
using Irt.SharedKernel.Results;
using Microsoft.AspNetCore.Mvc;
using IrtResultOfReportingScope = Irt.SharedKernel.Results.Result<Irt.Application.ReportingScopes.ReportingScopeDto>;


namespace IrtWeb.ReportingScopes;

public class ReportingScopesController(
    ICommandDispatcher commandDispatcher,
    IQueryDispatcher queryDispatcher) : Controller
{
    private const string ApiPrefix = "reporting-scope";
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get()
    {
        var result = await queryDispatcher
            .DispatchAsync<GetReportingScopeQuery, Irt.SharedKernel.Results.Result<List<ReportingScopeDto>>>(
                new GetReportingScopeQuery(), CancellationToken.None);

        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] string key)
    {
        var reportingScope = await queryDispatcher
            .DispatchAsync<GetReportingScopeByIdQuery, IrtResultOfReportingScope>(
                new GetReportingScopeByIdQuery(key), CancellationToken.None);
        return reportingScope.IsSuccess ? Ok(reportingScope.Value) : reportingScope.ToActionResult();
        //return reportingScope.ToActionResult();
    }
    
    [HttpPost(ApiPrefix)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateReportingScope([FromBody] CreateReportingScopeCommand command)
    {
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

    [HttpPut(ApiPrefix + "/{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateReportingScope(
        [FromRoute] string? key,
        [FromBody] UpdateReportingScopeCommand command)
    {
        if ( key is null || key != command.Id )
        {
            return BadRequest("Key in route does not match key in body.");
        }
        
        var result = await commandDispatcher
            .DispatchCommandAsync<UpdateReportingScopeCommand, IrtResultOfReportingScope>(
                command, CancellationToken.None);
        return result.ToActionResult();
    }
    
    [HttpPatch(ApiPrefix + "/{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PatchReportingScope(
        [FromRoute] string? key,
        [FromBody] PatchReportingScopeCommand command)
    {
        if ( key is null || key != command.Id )
        {
            return BadRequest("Key in route does not match key in body.");
        }
        
        var result = await commandDispatcher
            .DispatchCommandAsync<PatchReportingScopeCommand, IrtResultOfReportingScope>(
                command, CancellationToken.None);
        return result.ToActionResult();
    }
}
