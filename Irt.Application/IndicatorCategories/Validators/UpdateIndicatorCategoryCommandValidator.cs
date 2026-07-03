using FluentValidation;
using Irt.Application.IndicatorCategories.Commands;

namespace Irt.Application.IndicatorCategories.Validators;

public sealed class UpdateIndicatorCategoryCommandValidator
    : IndicatorCategoryUpsertCommandValidator<UpdateIndicatorCategoryCommand>
{
    public UpdateIndicatorCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
