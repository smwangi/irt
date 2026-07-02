using Irt.Application.Dispatchers;
using Irt.Application.IndicatorDefinitions;
using Irt.Application.IndicatorDefinitions.Commands;
using Irt.SharedKernel.Common;
using IrtWeb.GraphQL;
using Results = Irt.SharedKernel.Results;

namespace IrtWeb.GraphQL.IndicatorDefinitions;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class IndicatorDefinitionMutations
{
    public async Task<IndicatorDefinitionDto> CreateIndicatorDefinition(
        CreateIndicatorDefinitionCommand input,
        [Service] ICommandDispatcher dispatcher, CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                CreateIndicatorDefinitionCommand,
                Results.Result<IndicatorDefinitionDto>>(input, cancellationToken);

        return result.ValueOrThrow();
    }

    public async Task<IndicatorDefinitionDto> PatchIndicatorDefinition(
        PatchIndicatorDefinitionCommand input,
        [Service] ICommandDispatcher dispatcher, CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                PatchIndicatorDefinitionCommand,
                Results.Result<IndicatorDefinitionDto>>(input, cancellationToken);

        return result.ValueOrThrow();
    }

    public async Task<IndicatorDefinitionDto> UpdateIndicatorDefinition(
        UpdateIndicatorDefinitionCommand input,
        [Service] ICommandDispatcher dispatcher, CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                UpdateIndicatorDefinitionCommand,
                Results.Result<IndicatorDefinitionDto>>(input, cancellationToken);

        return result.ValueOrThrow();
    }

    public async Task<bool> DeleteIndicatorDefinition(
        DeleteIndicatorDefinitionCommand input,
        [Service] ICommandDispatcher dispatcher, CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                DeleteIndicatorDefinitionCommand,
                Results.Result<Unit>>(input, cancellationToken);

        result.ValueOrThrow();

        return true;
    }
}
