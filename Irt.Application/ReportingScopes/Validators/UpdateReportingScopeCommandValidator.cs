using FluentValidation;
using Irt.Application.ReportingScopes.Commands;

namespace Irt.Application.ReportingScopes.Validators;

public sealed class UpdateReportingScopeCommandValidator
    : ReportingScopeUpsertCommandValidator<UpdateReportingScopeCommand>
{
    public UpdateReportingScopeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
