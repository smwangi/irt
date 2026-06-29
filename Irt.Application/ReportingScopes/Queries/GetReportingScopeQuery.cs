using Irt.Application.Configuration.Queries;
using Irt.SharedKernel.Results;

namespace Irt.Application.ReportingScopes.Queries;

public sealed record GetReportingScopeQuery
    : IQuery<Result<List<ReportingScopeDto>>>;

public sealed record GetReportingScopeByIdQuery(string Id)
    : IQuery<Result<ReportingScopeDto>>;


