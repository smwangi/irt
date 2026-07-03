using Irt.Application.Dispatchers;
using Irt.Application.IndicatorDefinitions;
using Irt.Application.IndicatorDefinitions.Queries;
using Result = Irt.SharedKernel.Results.Result<System.Linq.IQueryable<Irt.Application.IndicatorDefinitions.IndicatorDefinitionDto>>;

namespace IrtWeb.GraphQL.IndicatorDefinitions;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class IndicatorDefinitionQueries
{
    [UseProjection, UseFiltering, UseSorting]
    public async Task<IQueryable<IndicatorDefinitionDto>> GetIndicatorDefinitions(
        [Service] IQueryDispatcher dispatcher,
        string? search = null,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher
            .DispatchAsync<QueryIndicatorDefinitions, Result>(
                new QueryIndicatorDefinitions(search), cancellationToken);
        return result.ValueOrThrow();
    }

    [UseProjection]
    public async Task<IQueryable<IndicatorDefinitionDto>> GetIndicatorDefinitionById(
        [Service] IQueryDispatcher dispatcher,
        string id,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher
            .DispatchAsync<QueryIndicatorDefinitionById, Result>(
                new QueryIndicatorDefinitionById(id), cancellationToken);
        return result.ValueOrThrow();
    }
}
