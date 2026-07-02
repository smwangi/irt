using Irt.Application.Common;
using Irt.Application.Configuration.Commands;
using Irt.SharedKernel.Results;

namespace Irt.Application.ReportingScopes.Commands;

public record CreateReportingScopeCommand(string Name, string Description)
    : ICommand<Result<ReportingScopeDto>>;
