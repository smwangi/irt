using Microsoft.EntityFrameworkCore;
using Irt.Application.Configuration.Commands;
using Irt.Core.IndicatorCategories;
using Irt.Core.IndicatorDefinitions;
using Irt.SharedKernel.Common;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorCategories.Commands.Handlers;

internal sealed class DeleteIndicatorCategoryCommandHandler(
    IRepository<IndicatorCategory> repository,
    IReadOnlyRepository<IndicatorDefinition> indicatorDefinitionRepository)
    : ICommandHandler<DeleteIndicatorCategoryCommand, Result<Unit>>
{
    public async Task<Result<Unit>> HandleAsync(
        DeleteIndicatorCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(IndicatorCategoryId.Create(command.Id), cancellationToken);

        if (entity is null || entity.IsDeleted)
        {
            return Result<Unit>.Failure(
                IrtError.NotFound($"IndicatorCategory with id {command.Id} not found."));
        }

        var isInUse = await indicatorDefinitionRepository
            .Query(indicator => !indicator.IsDeleted && indicator.IndicatorCategory.Id == entity.Id)
            .AnyAsync(cancellationToken);

        if (isInUse)
        {
            return Result<Unit>.Failure(
                IrtError.Conflict("Indicator category cannot be deleted while active indicator definitions use it."));
        }

        entity.MarkAsDeleted();
        await repository.UpdateAsync(entity, cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
