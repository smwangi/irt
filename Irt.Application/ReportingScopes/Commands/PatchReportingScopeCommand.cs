using System.Windows.Input;
using Irt.SharedKernel.Results;

namespace Irt.Application.ReportingScopes.Commands;

public sealed record PatchReportingScopeCommand : ICommand<Result<ReportingScopeDto>
{
    public string Id { get; init; } = default!;
    public string Name { get; init; }
    public string Description { get; init; }
}