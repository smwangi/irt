using AutoMapper;
using AutoMapper.QueryableExtensions;
using Irt.Application.Common;
using Irt.Application.Configuration.Queries;
using Irt.Core.UnitOfMeasurements;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.UnitOfMeasurements.Queries;

public sealed record QueryUnitOfMeasures(string? Search = null)
    : IQuery<Result<IQueryable<UnitOfMeasureDto>>>;

public sealed record QueryUnitOfMeasureById(string Id)
    : IQuery<Result<IQueryable<UnitOfMeasureDto>>>;

internal sealed class QueryUnitOfMeasuresHandler(
    IReadOnlyRepository<UnitOfMeasure> repository,
    IMapper mapper)
    : IQueryableQueryHandler<QueryUnitOfMeasures, Result<IQueryable<UnitOfMeasureDto>>>
{
    public Task<Result<IQueryable<UnitOfMeasureDto>>> HandleAsync(
        QueryUnitOfMeasures query, CancellationToken cancellationToken)
    {
        var q = repository
            .Query(x => !x.IsDeleted)
            .WhereContainsInsensitive(query.Search, x => x.Name.Value)
            .ProjectTo<UnitOfMeasureDto>(mapper.ConfigurationProvider);

        return Task.FromResult(Result<IQueryable<UnitOfMeasureDto>>.Success(q));
    }
}

internal sealed class QueryUnitOfMeasureByIdHandler(
    IReadOnlyRepository<UnitOfMeasure> repository,
    IMapper mapper)
    : IQueryableQueryHandler<QueryUnitOfMeasureById, Result<IQueryable<UnitOfMeasureDto>>>
{
    public Task<Result<IQueryable<UnitOfMeasureDto>>> HandleAsync(
        QueryUnitOfMeasureById query, CancellationToken cancellationToken)
    {
        var id = UnitOfMeasureId.Create(query.Id);
        var q = repository
            .Query(x => !x.IsDeleted && x.Id == id)
            .ProjectTo<UnitOfMeasureDto>(mapper.ConfigurationProvider);

        return Task.FromResult(Result<IQueryable<UnitOfMeasureDto>>.Success(q));
    }
}
