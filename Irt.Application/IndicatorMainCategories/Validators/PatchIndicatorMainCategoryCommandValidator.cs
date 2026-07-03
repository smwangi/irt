using FluentValidation;
using Irt.Application.IndicatorMainCategories.Commands;

namespace Irt.Application.IndicatorMainCategories.Validators;

public sealed class PatchIndicatorMainCategoryCommandValidator
    : IndicatorMainCategoryUpsertCommandValidator<PatchIndicatorMainCategoryCommand>
{
    public PatchIndicatorMainCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
