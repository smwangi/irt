using FluentValidation;
using Irt.Application.IndicatorMainCategories.Commands;

namespace Irt.Application.IndicatorMainCategories.Validators;

public sealed class DeleteIndicatorMainCategoryCommandValidator : AbstractValidator<DeleteIndicatorMainCategoryCommand>
{
    public DeleteIndicatorMainCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
