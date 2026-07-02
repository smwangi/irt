using FluentValidation;
using Irt.Application.IndicatorDefinitions.Commands;

namespace Irt.Application.IndicatorDefinitions.Validators;

public abstract class IndicatorDefinitionUpsertCommandValidator<TCommand>
    : AbstractValidator<TCommand>
    where TCommand : IIndicatorDefinitionUpsertCommand
{
    protected IndicatorDefinitionUpsertCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(x => x.ReportingScopeId)
            .NotEmpty().WithMessage("ReportingScopeId is required.");

        RuleFor(x => x.UnitOfMeasureId)
            .NotEmpty().WithMessage("UnitOfMeasureId is required.");

        RuleFor(x => x.IndicatorCategoryId)
            .NotEmpty().WithMessage("IndicatorCategoryId is required.");

        RuleFor(x => x.MaxThreshold)
            .GreaterThanOrEqualTo(x => x.MinThreshold)
            .WithMessage("MaxThreshold cannot be less than MinThreshold.");
    }
}
