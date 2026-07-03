using Irt.Application.Dispatchers;
using Irt.Application.IndicatorCategories;
using Irt.Application.IndicatorCategories.Queries;
using Result = Irt.SharedKernel.Results.Result<System.Linq.IQueryable<Irt.Application.IndicatorCategories.IndicatorCategoryDto>>;

namespace IrtWeb.GraphQL.IndicatorCategories;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class IndicatorCategoryQueries
{
    [UseProjection, UseFiltering, UseSorting]
    public async Task<IQueryable<IndicatorCategoryDto>> GetIndicatorCategories(
        [Service] IQueryDispatcher dispatcher,
        string? search = null,
        string? indicatorMainCategoryId = null,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher
            .DispatchAsync<QueryIndicatorCategories, Result>(
                new QueryIndicatorCategories(search, indicatorMainCategoryId), cancellationToken);
        return result.ValueOrThrow();
    }

    [UseProjection]
    public async Task<IQueryable<IndicatorCategoryDto>> GetIndicatorCategoryById(
        [Service] IQueryDispatcher dispatcher,
        string id,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher
            .DispatchAsync<QueryIndicatorCategoryById, Result>(
                new QueryIndicatorCategoryById(id), cancellationToken);
        return result.ValueOrThrow();
    }
}
