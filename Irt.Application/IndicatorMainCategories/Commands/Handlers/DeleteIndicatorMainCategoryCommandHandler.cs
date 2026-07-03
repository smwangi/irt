using Microsoft.EntityFrameworkCore;
using Irt.Application.Configuration.Commands;
using Irt.Core.IndicatorCategories;
using Irt.Core.IndicatorMainCategories;
using Irt.SharedKernel.Common;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorMainCategories.Commands.Handlers;

internal sealed class DeleteIndicatorMainCategoryCommandHandler(
    IRepository<IndicatorMainCategory> repository,
    IReadOnlyRepository<IndicatorCategory> indicatorCategoryRepository)
    : ICommandHandler<DeleteIndicatorMainCategoryCommand, Result<Unit>>
{
    public async Task<Result<Unit>> HandleAsync(
        DeleteIndicatorMainCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(IndicatorMainCategoryId.Create(command.Id), cancellationToken);

        if (entity is null || entity.IsDeleted)
        {
            return Result<Unit>.Failure(
                IrtError.NotFound($"IndicatorMainCategory with id {command.Id} not found."));
        }

        var hasActiveCategories = await indicatorCategoryRepository
            .Query(category => !category.IsDeleted && category.IndicatorMainCategory.Id == entity.Id)
            .AnyAsync(cancellationToken);

        if (hasActiveCategories)
        {
            return Result<Unit>.Failure(
                IrtError.Conflict("Indicator main category cannot be deleted while active indicator categories use it."));
        }

        entity.MarkAsDeleted();
        await repository.UpdateAsync(entity, cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
