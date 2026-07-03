using Irt.Application.Dispatchers;
using Irt.Application.UnitOfMeasurements;
using Irt.Application.UnitOfMeasurements.Commands;
using Irt.SharedKernel.Common;
using IrtWeb.GraphQL;
using Results = Irt.SharedKernel.Results;

namespace IrtWeb.GraphQL.UnitOfMeasurements;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class UnitOfMeasureMutations
{
    public async Task<UnitOfMeasureDto> CreateUnitOfMeasure(
        CreateUnitOfMeasureCommand input,
        [Service] ICommandDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                CreateUnitOfMeasureCommand,
                Results.Result<UnitOfMeasureDto>>(input, cancellationToken);

        return result.ValueOrThrow();
    }

    public async Task<UnitOfMeasureDto> PatchUnitOfMeasure(
        PatchUnitOfMeasureCommand input,
        [Service] ICommandDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                PatchUnitOfMeasureCommand,
                Results.Result<UnitOfMeasureDto>>(input, cancellationToken);

        return result.ValueOrThrow();
    }

    public async Task<UnitOfMeasureDto> UpdateUnitOfMeasure(
        UpdateUnitOfMeasureCommand input,
        [Service] ICommandDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                UpdateUnitOfMeasureCommand,
                Results.Result<UnitOfMeasureDto>>(input, cancellationToken);

        return result.ValueOrThrow();
    }

    public async Task<bool> DeleteUnitOfMeasure(
        DeleteUnitOfMeasureCommand input,
        [Service] ICommandDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        var result = await dispatcher
            .DispatchCommandAsync<
                DeleteUnitOfMeasureCommand,
                Results.Result<Unit>>(input, cancellationToken);

        return result.ValueOrThrow() == Unit.Value;
    }
}
