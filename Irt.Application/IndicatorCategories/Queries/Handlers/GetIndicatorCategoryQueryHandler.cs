using AutoMapper;
using AutoMapper.QueryableExtensions;
using Irt.Application.Common;
using Irt.Application.Configuration.Queries;
using Irt.Core.IndicatorCategories;
using Irt.Core.IndicatorMainCategories;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;
using Microsoft.EntityFrameworkCore;

namespace Irt.Application.IndicatorCategories.Queries.Handlers;

internal sealed class GetIndicatorCategoryQueryHandler(
    IReadOnlyRepository<IndicatorCategory> repository,
    IMapper mapper)
    : IQueryHandler<GetIndicatorCategoryQuery, Result<List<IndicatorCategoryDto>>>
{
    public async Task<Result<List<IndicatorCategoryDto>>> HandleAsync(
        GetIndicatorCategoryQuery query,
        CancellationToken cancellationToken)
    {
        var itemsQuery = repository
            .Query(category => !category.IsDeleted && !category.IndicatorMainCategory.IsDeleted)
            .WhereContainsInsensitive(query.Search, category => category.Name.Value);

        if (!string.IsNullOrWhiteSpace(query.IndicatorMainCategoryId))
        {
            var mainCategoryId = IndicatorMainCategoryId.Create(query.IndicatorMainCategoryId);
            itemsQuery = itemsQuery.Where(category => category.IndicatorMainCategory.Id == mainCategoryId);
        }

        var items = await itemsQuery
            .ProjectTo<IndicatorCategoryDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return Result<List<IndicatorCategoryDto>>.Success(items);
    }
}

internal sealed class GetIndicatorCategoryByIdQueryHandler(
    IReadOnlyRepository<IndicatorCategory> repository,
    IMapper mapper)
    : IQueryHandler<GetIndicatorCategoryByIdQuery, Result<IndicatorCategoryDto>>
{
    public async Task<Result<IndicatorCategoryDto>> HandleAsync(
        GetIndicatorCategoryByIdQuery query,
        CancellationToken cancellationToken)
    {
        var item = await repository
            .QueryById(query.Id)
            .Where(category => !category.IsDeleted && !category.IndicatorMainCategory.IsDeleted)
            .ProjectTo<IndicatorCategoryDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return item is not null
            ? Result<IndicatorCategoryDto>.Success(item)
            : Result<IndicatorCategoryDto>.Failure(
                IrtError.NotFound($"IndicatorCategory with id {query.Id} not found."));
    }
}
