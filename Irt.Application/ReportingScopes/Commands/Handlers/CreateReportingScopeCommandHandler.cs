using Irt.Application.Common;
using Irt.Application.Configuration.Commands;
using Irt.Core.ReportingScopes;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.Providers;
using Irt.SharedKernel.Results;

namespace Irt.Application.ReportingScopes.Commands.Handlers;

public class CreateReportingScopeCommandHandler(
    IRepositoryProvider repositoryProvider,
    IUserDetails userDetails)
    : ICommandHandler<CreateReportingScopeCommand, Result<ReportingScopeDto>>
{
    public async Task<Result<ReportingScopeDto>> HandleAsync(CreateReportingScopeCommand command, CancellationToken cancellationToken)
    {
        var reportingScopeRepository = repositoryProvider.GetRepository<ReportingScope>();
        
        return await reportingScopeRepository
            .ExistsAsync(x => x.Name == Name.Of(command.Name), cancellationToken)
            .EnsureNotExistsAsync("A reporting scope with that name already exists.")
            .BindAsync(async sp =>
            {
                var reportingScope = ReportingScope.CreateReportingScope(
                    name: Name.Of(command.Name), 
                    command.Description);
                reportingScope.RegisterCreation(
                    command.UserId ?? userDetails.UserId,
                    command.UserName ?? userDetails.UserName,
                    command.Application ?? userDetails.Application,
                    command.IpAddress ?? userDetails.IpAddress);
                
                reportingScope.RegisterModification(
                    command.UserId ?? userDetails.UserId,
                    command.UserName ?? userDetails.UserName,
                    command.Application ?? userDetails.Application,
                    command.IpAddress ?? userDetails.IpAddress);
                
                await reportingScopeRepository.AddAsync(reportingScope, cancellationToken);
                return Result<ReportingScopeDto>.Success(new ReportingScopeDto
                (
                    reportingScope.Name.Value,
                    reportingScope.Description
                ));
            });
        
    }
}