using Irt.Application.ReportingScopes.Commands;

namespace IrtWeb.ReportingScopes;

public sealed record CreateReportingScopeRequest(string Name, string Description)
{
    public CreateReportingScopeCommand ToCommand()
        => new(Name, Description);
}

public sealed record UpdateReportingScopeRequest(string Name, string Description)
{
    public UpdateReportingScopeCommand ToCommand(string? id)
        => new(Name, Description) { Id = id ?? string.Empty };
}

public sealed record PatchReportingScopeRequest(string Name, string Description)
{
    public PatchReportingScopeCommand ToCommand(string? id)
        => new(Name, Description) { Id = id ?? string.Empty };
}
