using FluentValidation;
using Irt.Application.IndicatorCategories.Commands;

namespace Irt.Application.IndicatorCategories.Validators;

public sealed class DeleteIndicatorCategoryCommandValidator : AbstractValidator<DeleteIndicatorCategoryCommand>
{
    public DeleteIndicatorCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
