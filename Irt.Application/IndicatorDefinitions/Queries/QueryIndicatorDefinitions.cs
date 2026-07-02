using AutoMapper;
using AutoMapper.QueryableExtensions;
using Irt.Application.Common;
using Irt.Application.Configuration.Queries;
using Irt.Core.IndicatorDefinitions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorDefinitions.Queries;

public sealed record QueryIndicatorDefinitions(string? Search = null)
    : IQuery<Result<IQueryable<IndicatorDefinitionDto>>>;

public sealed record QueryIndicatorDefinitionById(string Id)
    : IQuery<Result<IQueryable<IndicatorDefinitionDto>>>;

internal sealed class QueryIndicatorDefinitionsHandler(
    IReadOnlyRepository<IndicatorDefinition> repository,
    IMapper mapper)
    : IQueryableQueryHandler<QueryIndicatorDefinitions, Result<IQueryable<IndicatorDefinitionDto>>>
{
    public Task<Result<IQueryable<IndicatorDefinitionDto>>> HandleAsync(
        QueryIndicatorDefinitions query, CancellationToken cancellationToken)
    {
        var q = repository
            .Query(x => !x.IsDeleted)
            .WhereContainsInsensitive(query.Search, x => x.Name.Value)
            .ProjectTo<IndicatorDefinitionDto>(mapper.ConfigurationProvider);

        return Task.FromResult(Result<IQueryable<IndicatorDefinitionDto>>.Success(q));
    }
}

internal sealed class QueryIndicatorDefinitionByIdHandler(
    IReadOnlyRepository<IndicatorDefinition> repository,
    IMapper mapper)
    : IQueryableQueryHandler<QueryIndicatorDefinitionById, Result<IQueryable<IndicatorDefinitionDto>>>
{
    public Task<Result<IQueryable<IndicatorDefinitionDto>>> HandleAsync(
        QueryIndicatorDefinitionById query, CancellationToken cancellationToken)
    {
        var id = IndicatorDefinitionId.Create(query.Id);
        var q = repository
            .Query(x => !x.IsDeleted && x.Id == id)
            .ProjectTo<IndicatorDefinitionDto>(mapper.ConfigurationProvider);

        return Task.FromResult(Result<IQueryable<IndicatorDefinitionDto>>.Success(q));
    }
}
