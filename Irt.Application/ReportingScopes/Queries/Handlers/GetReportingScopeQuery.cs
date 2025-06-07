using Irt.Application.Configuration.Queries;
using Irt.SharedKernel.Results;

namespace Irt.Application.ReportingScopes.Queries.Handlers;

public class GetReportingScopeQuery : IQuery<Result<List<ReportingScopeDto>>>
{
}
public class GetReportingScopeByIdQuery(string id) : IQuery<Result<ReportingScopeDto>>
{
    public string Id { get; } = id;
}