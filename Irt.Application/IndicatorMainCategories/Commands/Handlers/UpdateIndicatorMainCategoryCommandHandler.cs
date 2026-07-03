using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Core.IndicatorMainCategories;
using Irt.Core.SharedKernel;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorMainCategories.Commands.Handlers;

internal sealed class UpdateIndicatorMainCategoryCommandHandler(
    IRepository<IndicatorMainCategory> repository,
    INameUniquenessChecker<IndicatorMainCategory, IndicatorMainCategoryId> uniquenessChecker,
    IMapper mapper)
    : ICommandHandler<UpdateIndicatorMainCategoryCommand, Result<IndicatorMainCategoryDto>>
{
    public async Task<Result<IndicatorMainCategoryDto>> HandleAsync(
        UpdateIndicatorMainCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(IndicatorMainCategoryId.Create(command.Id), cancellationToken);

        if (entity is null || entity.IsDeleted)
        {
            return Result<IndicatorMainCategoryDto>.Failure(
                IrtError.NotFound($"IndicatorMainCategory with id {command.Id} not found."));
        }

        if (!await uniquenessChecker.IsNameUniqueAsync(command.Name, entity.Id, cancellationToken))
        {
            return Result<IndicatorMainCategoryDto>.Failure(
                IrtError.Conflict($"An indicator main category named '{command.Name}' already exists."));
        }

        entity.Update(command.Name, command.Description);

        return Result<IndicatorMainCategoryDto>.Success(mapper.Map<IndicatorMainCategoryDto>(entity));
    }
}
