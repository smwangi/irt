using AutoMapper;
using AutoMapper.QueryableExtensions;
using Irt.Application.Common;
using Irt.Application.Configuration.Queries;
using Irt.Core.IndicatorMainCategories;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;
using Microsoft.EntityFrameworkCore;

namespace Irt.Application.IndicatorMainCategories.Queries.Handlers;

internal sealed class GetIndicatorMainCategoryQueryHandler(
    IReadOnlyRepository<IndicatorMainCategory> repository,
    IMapper mapper)
    : IQueryHandler<GetIndicatorMainCategoryQuery, Result<List<IndicatorMainCategoryDto>>>
{
    public async Task<Result<List<IndicatorMainCategoryDto>>> HandleAsync(
        GetIndicatorMainCategoryQuery query,
        CancellationToken cancellationToken)
    {
        var items = await repository
            .Query(item => !item.IsDeleted)
            .WhereContainsInsensitive(query.Search, item => item.Name.Value)
            .ProjectTo<IndicatorMainCategoryDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return Result<List<IndicatorMainCategoryDto>>.Success(items);
    }
}

internal sealed class GetIndicatorMainCategoryByIdQueryHandler(
    IReadOnlyRepository<IndicatorMainCategory> repository,
    IMapper mapper)
    : IQueryHandler<GetIndicatorMainCategoryByIdQuery, Result<IndicatorMainCategoryDto>>
{
    public async Task<Result<IndicatorMainCategoryDto>> HandleAsync(
        GetIndicatorMainCategoryByIdQuery query,
        CancellationToken cancellationToken)
    {
        var item = await repository
            .QueryById(query.Id)
            .Where(category => !category.IsDeleted)
            .ProjectTo<IndicatorMainCategoryDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return item is not null
            ? Result<IndicatorMainCategoryDto>.Success(item)
            : Result<IndicatorMainCategoryDto>.Failure(
                IrtError.NotFound($"IndicatorMainCategory with id {query.Id} not found."));
    }
}
