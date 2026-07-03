using FluentValidation;
using Irt.Application.ReportingScopes.Commands;

namespace Irt.Application.ReportingScopes.Validators;

public abstract class ReportingScopeUpsertCommandValidator<TCommand>
    : AbstractValidator<TCommand>
    where TCommand : IReportingScopeUpsertCommand
{
    protected ReportingScopeUpsertCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");
    }
}
