using AutoMapper;
using Irt.Application.Configuration.Commands;
using Irt.Core.IndicatorMainCategories;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.Application.IndicatorMainCategories.Commands.Handlers;

internal sealed class CreateIndicatorMainCategoryCommandHandler(
    IRepository<IndicatorMainCategory> repository,
    INameUniquenessChecker<IndicatorMainCategory, IndicatorMainCategoryId> uniquenessChecker,
    IMapper mapper)
    : ICommandHandler<CreateIndicatorMainCategoryCommand, Result<IndicatorMainCategoryDto>>
{
    public async Task<Result<IndicatorMainCategoryDto>> HandleAsync(
        CreateIndicatorMainCategoryCommand command,
        CancellationToken cancellationToken)
    {
        if (!await uniquenessChecker.IsNameUniqueAsync(command.Name, cancellationToken))
        {
            return Result<IndicatorMainCategoryDto>.Failure(
                IrtError.Conflict($"An indicator main category named '{command.Name}' already exists."));
        }

        var entity = IndicatorMainCategory.Create(Name.Of(command.Name), command.Description);

        await repository.AddAsync(entity, cancellationToken);

        return Result<IndicatorMainCategoryDto>.Success(mapper.Map<IndicatorMainCategoryDto>(entity));
    }
}
