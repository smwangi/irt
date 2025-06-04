using Asp.Versioning;
using Irt.Application.Dispatchers;
using Irt.Application.IndicatorDefinitions;
using Irt.Application.IndicatorDefinitions.Commands;
using Irt.Application.IndicatorDefinitions.Queries;
using Irt.SharedKernel.Results;
using Microsoft.AspNetCore.Mvc;

namespace IrtWeb.IndicatorDefinitions;

[ApiController]
[ControllerName("IndicatorDefinition")]
[Route("api/v{version:apiVersion}")]
[ApiVersion("1.0")]
public class IndicatorDefinitionController(
    ICommandDispatcher commandDispatcher,
    IQueryDispatcher queryDispatcher) : ControllerBase
{
    private const string ApiPrefix = "indicator-definitions";

    [HttpGet(ApiPrefix)]
    public async Task<IActionResult> GetAll()
    {
        var indicatorDefinitions = await queryDispatcher
            .DispatchAsync<GetIndicatorDefinitionQuery, Result<List<IndicatorDefinitionDto>>>(
                new GetIndicatorDefinitionQuery(), CancellationToken.None);

        return Ok(indicatorDefinitions);
    }
    
    [HttpGet(ApiPrefix + "/{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        var indicatorDefinition = await queryDispatcher
            .DispatchAsync<GetIndicatorDefinitionByIdQuery, Result<IndicatorDefinitionDto>>(
                new GetIndicatorDefinitionByIdQuery(id), CancellationToken.None);

        return Ok(indicatorDefinition);
    }
    
    [HttpPost(ApiPrefix)]
    public async Task<IActionResult> Create([FromBody] CreateIndicatorDefinitionCommand command)
    {
        var resp = await commandDispatcher
            .DispatchCommandAsync<CreateIndicatorDefinitionCommand, Result<IndicatorDefinitionDto>>(
                command, CancellationToken.None);
        
        if (!resp.IsSuccess)
        {
            return BadRequest(resp);
        }
        
        return Ok(resp);
    }
}