using Irt.Application.Common;
using Irt.Application.Configuration.Commands;
using Irt.Core.ReportingScopes;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.Providers;
using Irt.SharedKernel.Results;

namespace Irt.Application.ReportingScopes.Commands.Handlers;

public class CreateReportingScopeCommandHandler(
    IRepositoryProvider repositoryProvider)
    : ICommandHandler<CreateReportingScopeCommand, Result<ReportingScopeDto>>
{
    public async Task<Result<ReportingScopeDto>> HandleAsync(CreateReportingScopeCommand command, CancellationToken cancellationToken)
    {
        var reportingScopeRepository = repositoryProvider.GetRepository<ReportingScope>();

        return null; /*await reportingScopeRepository
            .ExistsAsync(x => x.Name == Name.Of(command.Name), cancellationToken)
            //.EnsureNotExistsAsync("A reporting scope with that name already exists.")
           .BindAsync(async sp =>
            {
                var reportingScope = ReportingScope.CreateReportingScope(
                    name: Name.Of(command.Name),
                    command.Description);
                AuditRegistrar.RegisterAuditFromCommand(
                    reportingScope,
                    command);

                await reportingScopeRepository.AddAsync(reportingScope, cancellationToken);
                return Result<ReportingScopeDto>.Success(new ReportingScopeDto
                (
                    Id: reportingScope.Id,
                    reportingScope.Name.Value,
                    reportingScope.Description
                ));
            })*/;
        
    }
}