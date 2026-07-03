namespace Irt.Application.ReportingScopes.Commands;

public interface IReportingScopeUpsertCommand
{
    string Name { get; }
    string Description { get; }
}
