using FluentValidation;
using Irt.Application.IndicatorMainCategories.Commands;

namespace Irt.Application.IndicatorMainCategories.Validators;

public sealed class UpdateIndicatorMainCategoryCommandValidator
    : IndicatorMainCategoryUpsertCommandValidator<UpdateIndicatorMainCategoryCommand>
{
    public UpdateIndicatorMainCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
