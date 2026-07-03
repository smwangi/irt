using FluentValidation;
using Irt.Application.IndicatorCategories.Commands;

namespace Irt.Application.IndicatorCategories.Validators;

public sealed class PatchIndicatorCategoryCommandValidator
    : IndicatorCategoryUpsertCommandValidator<PatchIndicatorCategoryCommand>
{
    public PatchIndicatorCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
