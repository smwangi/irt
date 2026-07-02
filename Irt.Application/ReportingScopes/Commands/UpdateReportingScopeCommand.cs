using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.ReportingScopes.Commands;

public sealed record UpdateReportingScopeCommand(
    string Name,
    string Description)
: ICommand<Result<ReportingScopeDto>>, IReportingScopeUpsertCommand
{
    public string Id { get; init; } = default!;
}
