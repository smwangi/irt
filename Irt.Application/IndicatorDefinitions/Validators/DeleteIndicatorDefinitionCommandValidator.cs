using FluentValidation;
using Irt.Application.IndicatorDefinitions.Commands;

namespace Irt.Application.IndicatorDefinitions.Validators;

public sealed class DeleteIndicatorDefinitionCommandValidator
    : AbstractValidator<DeleteIndicatorDefinitionCommand>
{
    public DeleteIndicatorDefinitionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
