using AutoMapper;
using AutoMapper.QueryableExtensions;
using Irt.Application.Common;
using Irt.Application.Configuration.Queries;
using Irt.Core.IndicatorDefinitions;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;
using Microsoft.EntityFrameworkCore;

namespace Irt.Application.IndicatorDefinitions.Queries.Handlers;

internal sealed class GetIndicatorDefinitionQueryHandler(
    IReadOnlyRepository<IndicatorDefinition> repository,
    IMapper mapper)
    : IQueryHandler<GetIndicatorDefinitionQuery, Result<List<IndicatorDefinitionDto>>>
{
    public async Task<Result<List<IndicatorDefinitionDto>>> HandleAsync(
        GetIndicatorDefinitionQuery query, CancellationToken cancellationToken)
    {
        var items = await repository
            .Query(e => !e.IsDeleted)
            .WhereContainsInsensitive(query.Search, e => e.Name.Value)
            .ProjectTo<IndicatorDefinitionDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return Result<List<IndicatorDefinitionDto>>.Success(items);
    }
}

internal sealed class GetIndicatorDefinitionByIdQueryHandler(
    IReadOnlyRepository<IndicatorDefinition> repository,
    IMapper mapper)
    : IQueryHandler<GetIndicatorDefinitionByIdQuery, Result<IndicatorDefinitionDto>>
{
    public async Task<Result<IndicatorDefinitionDto>> HandleAsync(
        GetIndicatorDefinitionByIdQuery query, CancellationToken cancellationToken)
    {
        var item = await repository
            .Query(e => !e.IsDeleted && e.Id.Value == query.Id)
            .ProjectTo<IndicatorDefinitionDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return item is not null
            ? Result<IndicatorDefinitionDto>.Success(item)
            : Result<IndicatorDefinitionDto>.Failure(
                IrtError.NotFound($"IndicatorDefinition with id {query.Id} not found"));
    }
}
