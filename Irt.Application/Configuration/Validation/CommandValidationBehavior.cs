using System.Text;
using FluentValidation;
namespace Irt.Application.Configuration.Validation
{
    public class CommandValidationBehavior<TRequest, TResponse> 
    {
        private readonly IList<IValidator<TRequest>> _validators;

        public CommandValidationBehavior(IList<IValidator<TRequest>> validators)
        {
            this._validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken )
        {
            var errors = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (errors.Any())
            {
                var errorBuilder = new StringBuilder();
                errorBuilder.AppendLine("Invalid command, reason: ");

                foreach (var error in errors)
                {
                    errorBuilder.AppendLine(error.ErrorMessage);
                }

                throw new InvalidCommandException(errorBuilder.ToString(), ""); // Fix: Pass an empty string as the second argument
            }

            return null;
        }
    }
}