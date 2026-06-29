using Irt.Application.Configuration.Commands;
using Irt.Core.ReportingScopes;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.ReportingScopes.Commands.Handlers;

public class PatchReportingScopeCommandHandler(
    IRepository<ReportingScope> repository)
: ICommandHandler<PatchReportingScopeCommand, Result<ReportingScopeDto>>
{
    public async Task<Result<ReportingScopeDto>> HandleAsync(
        PatchReportingScopeCommand command, CancellationToken cancellationToken)
    {
        var scope = await repository.GetByIdAsync(
            ReportingScopeId.Create(command.Id), cancellationToken);
        
        if (scope is null)
        {
            return Result<ReportingScopeDto>.Failure(
                IrtError.NotFound($"Reporting scope with id {command.Id} not found."));
        }

        scope.SetName(command.Name);

        scope.SetDescription(command.Description);

        await repository.UpdateAsync(scope, cancellationToken);
        return Result<ReportingScopeDto>.Success(ReportingScopeDto.Projection.Compile()(scope));
    }
}