using AutoMapper;
using AutoMapper.QueryableExtensions;
using Irt.Application.Common;
using Irt.Application.Configuration.Queries;
using Irt.Core.UnitOfMeasurements;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;
using Microsoft.EntityFrameworkCore;

namespace Irt.Application.UnitOfMeasurements.Queries.Handlers;

internal sealed class GetUnitOfMeasureQueryHandler(
    IReadOnlyRepository<UnitOfMeasure> repository,
    IMapper mapper)
    : IQueryHandler<GetUnitOfMeasureQuery, Result<List<UnitOfMeasureDto>>>
{
    public async Task<Result<List<UnitOfMeasureDto>>> HandleAsync(
        GetUnitOfMeasureQuery query,
        CancellationToken cancellationToken)
    {
        var items = await repository
            .Query(item => !item.IsDeleted)
            .WhereContainsInsensitive(query.Search, item => item.Name.Value)
            .ProjectTo<UnitOfMeasureDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return Result<List<UnitOfMeasureDto>>.Success(items);
    }
}

internal sealed class GetUnitOfMeasureByIdQueryHandler(
    IReadOnlyRepository<UnitOfMeasure> repository,
    IMapper mapper)
    : IQueryHandler<GetUnitOfMeasureByIdQuery, Result<UnitOfMeasureDto>>
{
    public async Task<Result<UnitOfMeasureDto>> HandleAsync(
        GetUnitOfMeasureByIdQuery query,
        CancellationToken cancellationToken)
    {
        var item = await repository
            .QueryById(query.Id)
            .Where(unit => !unit.IsDeleted)
            .ProjectTo<UnitOfMeasureDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return item is not null
            ? Result<UnitOfMeasureDto>.Success(item)
            : Result<UnitOfMeasureDto>.Failure(
                IrtError.NotFound($"UnitOfMeasure with id {query.Id} not found."));
    }
}
