using AutoMapper;
using AutoMapper.QueryableExtensions;
using Irt.Application.Common;
using Irt.Application.Configuration.Queries;
using Irt.Core.ReportingScopes;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;
using Microsoft.EntityFrameworkCore;

namespace Irt.Application.ReportingScopes.Queries.Handlers;

internal sealed class GetReportingScopeQueryHandler(
    IReadOnlyRepository<ReportingScope> repository,
    IMapper mapper)
    : IQueryHandler<GetReportingScopeQuery, Result<List<ReportingScopeDto>>>
{
    public async Task<Result<List<ReportingScopeDto>>> HandleAsync(
        GetReportingScopeQuery query, CancellationToken cancellationToken)
    {
        var queryable = repository
            .Query(scope => !scope.IsDeleted)
            .ProjectTo<ReportingScopeDto>(mapper.ConfigurationProvider);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.Trim().ToLower();

            queryable = queryable
                .Where(item => item.Name.ToLower().Contains(search));
        }

        var items = await queryable.ToListAsync(cancellationToken);
        return Result<List<ReportingScopeDto>>.Success(items);
    }
}

internal sealed class GetReportingScopeByIdQueryHandler(
    IReadOnlyRepository<ReportingScope> repository,
    IMapper mapper)
    : IQueryHandler<GetReportingScopeByIdQuery, Result<ReportingScopeDto>>
{
    public async Task<Result<ReportingScopeDto>> HandleAsync(
        GetReportingScopeByIdQuery query, CancellationToken cancellationToken)
    {
        var item = await repository
            .QueryById(query.Id)
            .ProjectTo<ReportingScopeDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        return item is not null
            ? Result<ReportingScopeDto>.Success(item)
            : Result<ReportingScopeDto>.Failure(
                IrtError.NotFound($"ReportingScope with id {query.Id} not found"));
    }
}
