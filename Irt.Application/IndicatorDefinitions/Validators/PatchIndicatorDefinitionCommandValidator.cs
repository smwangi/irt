using FluentValidation;
using Irt.Application.IndicatorDefinitions.Commands;

namespace Irt.Application.IndicatorDefinitions.Validators;

public sealed class PatchIndicatorDefinitionCommandValidator
    : AbstractValidator<PatchIndicatorDefinitionCommand>
{
    public PatchIndicatorDefinitionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        When(x => x.Name is not null, () =>
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty when provided.");
        });

        When(x => x.Description is not null, () =>
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty when provided.");
        });

        When(x => x.ReportingScopeId is not null, () =>
        {
            RuleFor(x => x.ReportingScopeId)
                .NotEmpty().WithMessage("ReportingScopeId cannot be empty when provided.");
        });

        When(x => x.UnitOfMeasureId is not null, () =>
        {
            RuleFor(x => x.UnitOfMeasureId)
                .NotEmpty().WithMessage("UnitOfMeasureId cannot be empty when provided.");
        });

        When(x => x.IndicatorCategoryId is not null, () =>
        {
            RuleFor(x => x.IndicatorCategoryId)
                .NotEmpty().WithMessage("IndicatorCategoryId cannot be empty when provided.");
        });

        When(x => x.MinThreshold is not null && x.MaxThreshold is not null, () =>
        {
            RuleFor(x => x.MaxThreshold!.Value)
                .GreaterThanOrEqualTo(x => x.MinThreshold!.Value)
                .WithMessage("MaxThreshold cannot be less than MinThreshold.");
        });
    }
}
