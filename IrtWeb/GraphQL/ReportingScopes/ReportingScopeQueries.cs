using Irt.Application.Dispatchers;
using Irt.Application.ReportingScopes;
using Irt.Application.ReportingScopes.Queries;
using Result = Irt.SharedKernel.Results.Result<System.Linq.IQueryable<Irt.Application.ReportingScopes.ReportingScopeDto>>;

namespace IrtWeb.GraphQL.ReportingScopes;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class ReportingScopeQueries
{
    [UseProjection, UseFiltering, UseSorting]
    public async Task<IQueryable<ReportingScopeDto>> GetReportingScopes(
        [Service] IQueryDispatcher dispatcher,
        string? search = null,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher
            .DispatchAsync<QueryReportingScopes, Result>(
                new QueryReportingScopes(search), cancellationToken);
        return result.ValueOrThrow();
    }

    [UseProjection]
    public async Task<IQueryable<ReportingScopeDto>> GetReportingScopeById(
        [Service] IQueryDispatcher dispatcher,
        string id,
        CancellationToken cancellationToken = default)
    {
        var result = await dispatcher
            .DispatchAsync<QueryReportingScopeById, Result>(
                new QueryReportingScopeById(id), cancellationToken);
        return result.ValueOrThrow();
    }
}
