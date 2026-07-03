using FluentValidation;
using Irt.Application.UnitOfMeasurements.Commands;

namespace Irt.Application.UnitOfMeasurements.Validators;

public sealed class DeleteUnitOfMeasureCommandValidator : AbstractValidator<DeleteUnitOfMeasureCommand>
{
    public DeleteUnitOfMeasureCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
