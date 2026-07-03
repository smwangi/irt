using Irt.Application.Dispatchers;
using Irt.Application.IndicatorMainCategories;
using Irt.Application.IndicatorMainCategories.Queries;
using Result = Irt.SharedKernel.Results.Result<System.Linq.IQueryable<Irt.Application.IndicatorMainCategories.IndicatorMainCategoryDto>>;

namespace IrtWeb.GraphQL.IndicatorMainCategories;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class IndicatorMainCategoryQueries
{
    [UseProjection, UseFiltering, UseSorting]
    public async Task<IQueryable<IndicatorMainCategoryDto>> GetIndicatorMainCategories(
        [Service] IQueryDispatcher dispatcher,
        string? search = null,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher
            .DispatchAsync<QueryIndicatorMainCategories, Result>(
                new QueryIndicatorMainCategories(search), cancellationToken);
        return result.ValueOrThrow();
    }

    [UseProjection]
    public async Task<IQueryable<IndicatorMainCategoryDto>> GetIndicatorMainCategoryById(
        [Service] IQueryDispatcher dispatcher,
        string id,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher
            .DispatchAsync<QueryIndicatorMainCategoryById, Result>(
                new QueryIndicatorMainCategoryById(id), cancellationToken);
        return result.ValueOrThrow();
    }
}
