using FluentValidation;
using Irt.Application.IndicatorDefinitions.Commands;

namespace Irt.Application.IndicatorDefinitions.Validators;

public sealed class UpdateIndicatorDefinitionCommandValidator
    : IndicatorDefinitionUpsertCommandValidator<UpdateIndicatorDefinitionCommand>
{
    public UpdateIndicatorDefinitionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
