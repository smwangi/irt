using Irt.Application.Dispatchers;
using Irt.Application.UnitOfMeasurements;
using Irt.Application.UnitOfMeasurements.Queries;
using Result = Irt.SharedKernel.Results.Result<System.Linq.IQueryable<Irt.Application.UnitOfMeasurements.UnitOfMeasureDto>>;

namespace IrtWeb.GraphQL.UnitOfMeasurements;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class UnitOfMeasureQueries
{
    [UseProjection, UseFiltering, UseSorting]
    public async Task<IQueryable<UnitOfMeasureDto>> GetUnitOfMeasures(
        [Service] IQueryDispatcher dispatcher,
        string? search = null,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher
            .DispatchAsync<QueryUnitOfMeasures, Result>(
                new QueryUnitOfMeasures(search), cancellationToken);
        return result.ValueOrThrow();
    }

    [UseProjection]
    public async Task<IQueryable<UnitOfMeasureDto>> GetUnitOfMeasureById(
        [Service] IQueryDispatcher dispatcher,
        string id,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher
            .DispatchAsync<QueryUnitOfMeasureById, Result>(
                new QueryUnitOfMeasureById(id), cancellationToken);
        return result.ValueOrThrow();
    }
}
