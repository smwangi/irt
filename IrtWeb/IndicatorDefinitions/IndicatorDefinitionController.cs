using Asp.Versioning;
using Irt.Application.Dispatchers;
using Irt.Application.IndicatorDefinitions;
using Irt.Application.IndicatorDefinitions.Commands;
using Irt.Application.IndicatorDefinitions.Queries;
using Irt.SharedKernel.Common;
using Irt.SharedKernel.Results;
using Microsoft.AspNetCore.Mvc;
using IrtResultOfIndicatorDefinition = Irt.SharedKernel.Results.Result<Irt.Application.IndicatorDefinitions.IndicatorDefinitionDto>;
using IrtResultOfIndicatorDefinitions = Irt.SharedKernel.Results.Result<System.Collections.Generic.List<Irt.Application.IndicatorDefinitions.IndicatorDefinitionDto>>;
using IrtResultOfUnit = Irt.SharedKernel.Results.Result<Irt.SharedKernel.Common.Unit>;

namespace IrtWeb.IndicatorDefinitions;

[ApiController]
[ControllerName("IndicatorDefinition")]
[Route("irt/api/v{version:apiVersion}")]
[ApiVersion("1.0")]
public class IndicatorDefinitionController(
    ICommandDispatcher commandDispatcher,
    IQueryDispatcher queryDispatcher) : ControllerBase
{
    private const string ApiPrefix = "indicator-definitions";

    [HttpGet(ApiPrefix)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> List([FromQuery] string? search = null)
    {
        var result = await queryDispatcher
            .DispatchAsync<GetIndicatorDefinitionQuery, IrtResultOfIndicatorDefinitions>(
                new GetIndicatorDefinitionQuery(search), CancellationToken.None);

        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }

    [HttpGet(ApiPrefix + "/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        var result = await queryDispatcher
            .DispatchAsync<GetIndicatorDefinitionByIdQuery, IrtResultOfIndicatorDefinition>(
                new GetIndicatorDefinitionByIdQuery(id), CancellationToken.None);

        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }

    [HttpPost(ApiPrefix)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateIndicatorDefinitionRequest request)
    {
        var command = request.ToCommand();
        var result = await commandDispatcher
            .DispatchCommandAsync<CreateIndicatorDefinitionCommand, IrtResultOfIndicatorDefinition>(
                command, CancellationToken.None);

        return result.Match(
            onSuccess: dto => CreatedAtAction(
                nameof(GetById),
                new { id = dto.Id, version = "1.0" },
                dto),
            onFailure: _ => result.ToActionResult());
    }

    [HttpPut(ApiPrefix + "/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(
        [FromRoute] string id,
        [FromBody] UpdateIndicatorDefinitionRequest request)
    {
        var command = request.ToCommand(id);
        var result = await commandDispatcher
            .DispatchCommandAsync<UpdateIndicatorDefinitionCommand, IrtResultOfIndicatorDefinition>(
                command, CancellationToken.None);

        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }

    [HttpPatch(ApiPrefix + "/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Patch(
        [FromRoute] string id,
        [FromBody] PatchIndicatorDefinitionRequest request)
    {
        var command = request.ToCommand(id);
        var result = await commandDispatcher
            .DispatchCommandAsync<PatchIndicatorDefinitionCommand, IrtResultOfIndicatorDefinition>(
                command, CancellationToken.None);

        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }

    [HttpDelete(ApiPrefix + "/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        var result = await commandDispatcher
            .DispatchCommandAsync<DeleteIndicatorDefinitionCommand, IrtResultOfUnit>(
                new DeleteIndicatorDefinitionCommand(id), CancellationToken.None);

        return result.IsSuccess ? NoContent() : result.ToActionResult();
    }
}
