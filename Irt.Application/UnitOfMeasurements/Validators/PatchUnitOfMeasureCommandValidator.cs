using FluentValidation;
using Irt.Application.UnitOfMeasurements.Commands;

namespace Irt.Application.UnitOfMeasurements.Validators;

public sealed class PatchUnitOfMeasureCommandValidator
    : UnitOfMeasureUpsertCommandValidator<PatchUnitOfMeasureCommand>
{
    public PatchUnitOfMeasureCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
