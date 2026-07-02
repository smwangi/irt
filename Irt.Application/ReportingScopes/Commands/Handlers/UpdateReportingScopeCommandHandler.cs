using Irt.Application.Common;
using Irt.Application.Configuration.Commands;
using Irt.Core.ReportingScopes;
using Irt.Core.SharedKernel;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.ReportingScopes.Commands.Handlers;

internal sealed class UpdateReportingScopeCommandHandler(
    IRepository<ReportingScope> repository,
    INameUniquenessChecker<ReportingScope, ReportingScopeId> uniquenessChecker)
: ICommandHandler<UpdateReportingScopeCommand, Result<ReportingScopeDto>>
{
    public async Task<Result<ReportingScopeDto>> HandleAsync(
        UpdateReportingScopeCommand command, CancellationToken cancellationToken)
    {
        var scope = await repository.GetByIdAsync(
            ReportingScopeId.Create(command.Id), cancellationToken);

        if (scope is null)
        {
            return Result<ReportingScopeDto>.Failure(
                IrtError.NotFound($"Reporting scope with id {command.Id} not found."));
        }

        if (!await uniquenessChecker.IsNameUniqueAsync(
                command.Name,
                scope.Id,
                cancellationToken))
        {
            return Result<ReportingScopeDto>.Failure(
                IrtError.Conflict($"A reporting scope named '{command.Name}' already exists."));
        }
        
        scope.Update(command.Name, command.Description); // full replace
        
        return Result<ReportingScopeDto>.Success(ReportingScopeDto.Projection.Compile()(scope));
    }
}
