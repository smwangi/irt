using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Core.IndicatorCategories;
using Irt.Core.IndicatorMainCategories;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorCategories.Commands.Handlers;

internal sealed class CreateIndicatorCategoryCommandHandler(
    IRepository<IndicatorCategory> repository,
    IRepository<IndicatorMainCategory> mainCategoryRepository,
    INameUniquenessChecker<IndicatorCategory, IndicatorCategoryId> uniquenessChecker,
    IMapper mapper)
    : ICommandHandler<CreateIndicatorCategoryCommand, Result<IndicatorCategoryDto>>
{
    public async Task<Result<IndicatorCategoryDto>> HandleAsync(
        CreateIndicatorCategoryCommand command,
        CancellationToken cancellationToken)
    {
        if (!await uniquenessChecker.IsNameUniqueAsync(command.Name, cancellationToken))
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

        var entity = IndicatorCategory.CreateIndicatorCategory(
            command.Description,
            Name.Of(command.Name),
            mainCategory);

        await repository.AddAsync(entity, cancellationToken);

        return Result<IndicatorCategoryDto>.Success(mapper.Map<IndicatorCategoryDto>(entity));
    }
}
