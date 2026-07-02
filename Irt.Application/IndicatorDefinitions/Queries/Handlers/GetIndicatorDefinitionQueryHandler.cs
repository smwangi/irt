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
    IProjection<IndicatorDefinition, IndicatorDefinitionDto> projection)
    : IQueryHandler<GetIndicatorDefinitionQuery, Result<List<IndicatorDefinitionDto>>>
{
    public async Task<Result<List<IndicatorDefinitionDto>>> HandleAsync(
        GetIndicatorDefinitionQuery query, CancellationToken cancellationToken)
    {
        var items = await repository
            .Query(e => !e.IsDeleted, projection.Expression)
            .ToListAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.Trim();
            items = items
                .Where(item => item.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        return Result<List<IndicatorDefinitionDto>>.Success(items);
    }
}

internal sealed class GetIndicatorDefinitionByIdQueryHandler(
    IReadOnlyRepository<IndicatorDefinition> repository,
    IProjection<IndicatorDefinition, IndicatorDefinitionDto> projection)
    : IQueryHandler<GetIndicatorDefinitionByIdQuery, Result<IndicatorDefinitionDto>>
{
    public async Task<Result<IndicatorDefinitionDto>> HandleAsync(
        GetIndicatorDefinitionByIdQuery query, CancellationToken cancellationToken)
    {
        var item = await repository
            .Query(e => !e.IsDeleted && e.Id.Value == query.Id, projection.Expression)
            .FirstOrDefaultAsync(cancellationToken);

        return item is not null
            ? Result<IndicatorDefinitionDto>.Success(item)
            : Result<IndicatorDefinitionDto>.Failure(
                IrtError.NotFound($"IndicatorDefinition with id {query.Id} not found"));
    }
}
