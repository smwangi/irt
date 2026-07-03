using AutoMapper;
using AutoMapper.QueryableExtensions;
using Irt.Application.Common;
using Irt.Application.Configuration.Queries;
using Irt.Core.ReportingScopes;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.ReportingScopes.Queries;

public sealed record QueryReportingScopes(string? Search = null)
    : IQuery<Result<IQueryable<ReportingScopeDto>>>;

public sealed record QueryReportingScopeById(string Id)
    : IQuery<Result<IQueryable<ReportingScopeDto>>>;

internal sealed class QueryReportingScopesHandler(
    IReadOnlyRepository<ReportingScope> repository,
    IMapper mapper)
    : IQueryableQueryHandler<QueryReportingScopes, Result<IQueryable<ReportingScopeDto>>>
{
    public Task<Result<IQueryable<ReportingScopeDto>>> HandleAsync(
        QueryReportingScopes query, CancellationToken cancellationToken)
    {
        var q = repository
            .Query(x => !x.IsDeleted)
            .WhereContainsInsensitive(query.Search, x => x.Name.Value)
            .ProjectTo<ReportingScopeDto>(mapper.ConfigurationProvider);

        return Task.FromResult(Result<IQueryable<ReportingScopeDto>>.Success(q));
    }
}

internal sealed class QueryReportingScopeByIdHandler(
    IReadOnlyRepository<ReportingScope> repository,
    IMapper mapper)
    : IQueryableQueryHandler<QueryReportingScopeById, Result<IQueryable<ReportingScopeDto>>>
{
    public Task<Result<IQueryable<ReportingScopeDto>>> HandleAsync(
        QueryReportingScopeById query, CancellationToken cancellationToken)
    {
        var id = ReportingScopeId.Create(query.Id);
        var q = repository
            .Query(x => !x.IsDeleted && x.Id == id)
            .ProjectTo<ReportingScopeDto>(mapper.ConfigurationProvider);

        return Task.FromResult(Result<IQueryable<ReportingScopeDto>>.Success(q));
    }
}
