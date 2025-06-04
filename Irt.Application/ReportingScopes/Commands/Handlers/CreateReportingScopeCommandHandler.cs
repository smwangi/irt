using Irt.Application.Configuration.Commands;
using Irt.Core.ReportingScopes;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.Providers;
using Irt.SharedKernel.Results;
using Irt.SharedKernel.ErrorHandling.Exceptions;

namespace Irt.Application.ReportingScopes.Commands.Handlers;

public class CreateReportingScopeCommandHandler(
    IRepositoryProvider repositoryProvider)
    : ICommandHandler<CreateReportingScopeCommand, Result<ReportingScopeDto>>
{
    public Task<Result<ReportingScopeDto>> HandleAsync(CreateReportingScopeCommand command, CancellationToken cancellationToken)
    {
        var reportingScopeRepository = repositoryProvider.GetRepository<ReportingScope>();
        
        var whereClause = $"WHERE Name = '{command.Name}'";
        return reportingScopeRepository.FilterByWhereClauseAsync(whereClause, cancellationToken)
            .EnsureAsync(rs => rs is null && rs.Count < 1,
                Error.FromException(new BadRequestException($"Reporting scope with name '{command.Name}' already exists.")))
            .BindAsync(async sp =>
            {
                var reportingScope = ReportingScope.CreateReportingScope(
                    name: Name.Of(command.Name), 
                    command.Description);
                
                await reportingScopeRepository.AddAsync(reportingScope, cancellationToken);
                return Result<ReportingScopeDto>.Success(new ReportingScopeDto
                (
                    reportingScope.Name.Value,
                    reportingScope.Description
                ));
            });
        
    }
}