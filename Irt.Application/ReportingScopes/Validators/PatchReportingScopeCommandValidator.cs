using FluentValidation;
using Irt.Application.ReportingScopes.Commands;

namespace Irt.Application.ReportingScopes.Validators;

public sealed class PatchReportingScopeCommandValidator
    : ReportingScopeUpsertCommandValidator<PatchReportingScopeCommand>
{
    public PatchReportingScopeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
