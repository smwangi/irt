using FluentValidation;
using Irt.Application.IndicatorCategories.Commands;

namespace Irt.Application.IndicatorCategories.Validators;

public abstract class IndicatorCategoryUpsertCommandValidator<TCommand>
    : AbstractValidator<TCommand>
    where TCommand : IIndicatorCategoryUpsertCommand
{
    protected IndicatorCategoryUpsertCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(x => x.IndicatorMainCategoryId)
            .NotEmpty().WithMessage("IndicatorMainCategoryId is required.");
    }
}
