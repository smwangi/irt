using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Core.IndicatorCategories;
using Irt.Core.IndicatorMainCategories;
using Irt.Core.SharedKernel;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorCategories.Commands.Handlers;

internal sealed class PatchIndicatorCategoryCommandHandler(
    IRepository<IndicatorCategory> repository,
    IRepository<IndicatorMainCategory> mainCategoryRepository,
    INameUniquenessChecker<IndicatorCategory, IndicatorCategoryId> uniquenessChecker,
    IMapper mapper)
    : ICommandHandler<PatchIndicatorCategoryCommand, Result<IndicatorCategoryDto>>
{
    public async Task<Result<IndicatorCategoryDto>> HandleAsync(
        PatchIndicatorCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(IndicatorCategoryId.Create(command.Id), cancellationToken);

        if (entity is null || entity.IsDeleted)
        {
            return Result<IndicatorCategoryDto>.Failure(
                IrtError.NotFound($"IndicatorCategory with id {command.Id} not found."));
        }

        if (!await uniquenessChecker.IsNameUniqueAsync(command.Name, entity.Id, cancellationToken))
        {
            return Result<IndicatorCategoryDto>.Failure(
                IrtError.Conflict($"An indicator category named '{command.Name}' already exists."));
        }

        var mainCategory = await mainCategoryRepository.GetByIdAsync(
            IndicatorMainCategoryId.Create(command.IndicatorMainCategoryId),
            cancellationToken);

        if (mainCategory is null || mainCategory.IsDeleted)
        {
            return Result<IndicatorCategoryDto>.Failure(
                IrtError.NotFound($"IndicatorMainCategory with id {command.IndicatorMainCategoryId} not found."));
        }

        entity.Update(command.Name, command.Description, mainCategory);

        return Result<IndicatorCategoryDto>.Success(mapper.Map<IndicatorCategoryDto>(entity));
    }
}
