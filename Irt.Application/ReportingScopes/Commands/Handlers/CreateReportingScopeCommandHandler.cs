using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Core.ReportingScopes;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.ReportingScopes.Commands.Handlers;

internal sealed class CreateReportingScopeCommandHandler(
    IRepository<ReportingScope> repository,
    INameUniquenessChecker<ReportingScope, ReportingScopeId> uniquenessChecker,
    IMapper mapper)
    : ICommandHandler<CreateReportingScopeCommand, Result<ReportingScopeDto>>
{
    public async Task<Result<ReportingScopeDto>> HandleAsync(CreateReportingScopeCommand command, CancellationToken cancellationToken)
    {
        if (!await uniquenessChecker.IsNameUniqueAsync(command.Name, cancellationToken))
        {
            return Result<ReportingScopeDto>.Failure(
                IrtError.Conflict($"A reporting scope named '{command.Name}' already exists."));
        }

        var reportingScope = ReportingScope.CreateReportingScope(
            name: Name.Of(command.Name),
            description: command.Description);

        await repository.AddAsync(reportingScope, cancellationToken);

        return Result<ReportingScopeDto>.Success(mapper.Map<ReportingScopeDto>(reportingScope));
        
    }
}
