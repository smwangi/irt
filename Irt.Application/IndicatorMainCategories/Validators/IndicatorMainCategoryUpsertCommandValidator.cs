using FluentValidation;
using Irt.Application.IndicatorMainCategories.Commands;

namespace Irt.Application.IndicatorMainCategories.Validators;

public abstract class IndicatorMainCategoryUpsertCommandValidator<TCommand>
    : AbstractValidator<TCommand>
    where TCommand : IIndicatorMainCategoryUpsertCommand
{
    protected IndicatorMainCategoryUpsertCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");
    }
}
