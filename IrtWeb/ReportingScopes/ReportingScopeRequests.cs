using Irt.Application.ReportingScopes.Commands;

namespace IrtWeb.ReportingScopes;

public sealed record CreateReportingScopeRequest(string Name, string Description)
{
    public CreateReportingScopeCommand ToCommand()
        => new(Name, Description);
}

public sealed record UpdateReportingScopeRequest(string Name, string Description)
{
    public UpdateReportingScopeCommand ToCommand(string? key)
        => new(Name, Description) { Id = key ?? string.Empty };
}

public sealed record PatchReportingScopeRequest(string Name, string Description)
{
    public PatchReportingScopeCommand ToCommand(string? key)
        => new(Name, Description) { Id = key ?? string.Empty };
}
