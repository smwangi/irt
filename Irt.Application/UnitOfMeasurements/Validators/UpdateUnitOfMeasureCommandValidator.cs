using FluentValidation;
using Irt.Application.UnitOfMeasurements.Commands;

namespace Irt.Application.UnitOfMeasurements.Validators;

public sealed class UpdateUnitOfMeasureCommandValidator
    : UnitOfMeasureUpsertCommandValidator<UpdateUnitOfMeasureCommand>
{
    public UpdateUnitOfMeasureCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
