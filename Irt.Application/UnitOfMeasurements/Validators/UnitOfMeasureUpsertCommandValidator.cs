using FluentValidation;
using Irt.Application.UnitOfMeasurements.Commands;

namespace Irt.Application.UnitOfMeasurements.Validators;

public abstract class UnitOfMeasureUpsertCommandValidator<TCommand>
    : AbstractValidator<TCommand>
    where TCommand : IUnitOfMeasureUpsertCommand
{
    protected UnitOfMeasureUpsertCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");
    }
}
