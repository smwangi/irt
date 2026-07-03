using AutoMapper;
using AutoMapper.QueryableExtensions;
using Irt.Application.Common;
using Irt.Application.Configuration.Queries;
using Irt.Core.IndicatorMainCategories;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorMainCategories.Queries;

public sealed record QueryIndicatorMainCategories(string? Search = null)
    : IQuery<Result<IQueryable<IndicatorMainCategoryDto>>>;

public sealed record QueryIndicatorMainCategoryById(string Id)
    : IQuery<Result<IQueryable<IndicatorMainCategoryDto>>>;

internal sealed class QueryIndicatorMainCategoriesHandler(
    IReadOnlyRepository<IndicatorMainCategory> repository,
    IMapper mapper)
    : IQueryableQueryHandler<QueryIndicatorMainCategories, Result<IQueryable<IndicatorMainCategoryDto>>>
{
    public Task<Result<IQueryable<IndicatorMainCategoryDto>>> HandleAsync(
        QueryIndicatorMainCategories query, CancellationToken cancellationToken)
    {
        var q = repository
            .Query(x => !x.IsDeleted)
            .WhereContainsInsensitive(query.Search, x => x.Name.Value)
            .ProjectTo<IndicatorMainCategoryDto>(mapper.ConfigurationProvider);

        return Task.FromResult(Result<IQueryable<IndicatorMainCategoryDto>>.Success(q));
    }
}

internal sealed class QueryIndicatorMainCategoryByIdHandler(
    IReadOnlyRepository<IndicatorMainCategory> repository,
    IMapper mapper)
    : IQueryableQueryHandler<QueryIndicatorMainCategoryById, Result<IQueryable<IndicatorMainCategoryDto>>>
{
    public Task<Result<IQueryable<IndicatorMainCategoryDto>>> HandleAsync(
        QueryIndicatorMainCategoryById query, CancellationToken cancellationToken)
    {
        var id = IndicatorMainCategoryId.Create(query.Id);
        var q = repository
            .Query(x => !x.IsDeleted && x.Id == id)
            .ProjectTo<IndicatorMainCategoryDto>(mapper.ConfigurationProvider);

        return Task.FromResult(Result<IQueryable<IndicatorMainCategoryDto>>.Success(q));
    }
}
