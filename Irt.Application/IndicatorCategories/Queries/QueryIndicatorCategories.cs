using AutoMapper;
using AutoMapper.QueryableExtensions;
using Irt.Application.Common;
using Irt.Application.Configuration.Queries;
using Irt.Core.IndicatorCategories;
using Irt.Core.IndicatorMainCategories;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorCategories.Queries;

public sealed record QueryIndicatorCategories(
    string? Search = null,
    string? IndicatorMainCategoryId = null)
    : IQuery<Result<IQueryable<IndicatorCategoryDto>>>;

public sealed record QueryIndicatorCategoryById(string Id)
    : IQuery<Result<IQueryable<IndicatorCategoryDto>>>;

internal sealed class QueryIndicatorCategoriesHandler(
    IReadOnlyRepository<IndicatorCategory> repository,
    IMapper mapper)
    : IQueryableQueryHandler<QueryIndicatorCategories, Result<IQueryable<IndicatorCategoryDto>>>
{
    public Task<Result<IQueryable<IndicatorCategoryDto>>> HandleAsync(
        QueryIndicatorCategories query, CancellationToken cancellationToken)
    {
        var q = repository
            .Query(x => !x.IsDeleted && !x.IndicatorMainCategory.IsDeleted)
            .WhereContainsInsensitive(query.Search, x => x.Name.Value);

        if (!string.IsNullOrWhiteSpace(query.IndicatorMainCategoryId))
        {
            var mainCategoryId = IndicatorMainCategoryId.Create(query.IndicatorMainCategoryId);
            q = q.Where(x => x.IndicatorMainCategory.Id == mainCategoryId);
        }

        var projected = q.ProjectTo<IndicatorCategoryDto>(mapper.ConfigurationProvider);
        return Task.FromResult(Result<IQueryable<IndicatorCategoryDto>>.Success(projected));
    }
}

internal sealed class QueryIndicatorCategoryByIdHandler(
    IReadOnlyRepository<IndicatorCategory> repository,
    IMapper mapper)
    : IQueryableQueryHandler<QueryIndicatorCategoryById, Result<IQueryable<IndicatorCategoryDto>>>
{
    public Task<Result<IQueryable<IndicatorCategoryDto>>> HandleAsync(
        QueryIndicatorCategoryById query, CancellationToken cancellationToken)
    {
        var id = IndicatorCategoryId.Create(query.Id);
        var q = repository
            .Query(x => !x.IsDeleted && !x.IndicatorMainCategory.IsDeleted && x.Id == id)
            .ProjectTo<IndicatorCategoryDto>(mapper.ConfigurationProvider);

        return Task.FromResult(Result<IQueryable<IndicatorCategoryDto>>.Success(q));
    }
}
