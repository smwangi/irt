using Irt.Application.Configuration.Commands;
using Irt.Core.ReportingScopes;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.ReportingScopes.Commands.Handlers;

internal sealed class UpdateReportingScopeCommandHandler(
    IRepository<ReportingScope> repository)
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
        
        scope.Update(command.Name, command.Description); // full replace
        await repository.SaveChangesAsync(scope, cancellationToken);
        
        return Result<ReportingScopeDto>.Success(ReportingScopeDto.Projection.Compile()(scope));
    }
}