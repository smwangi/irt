using Irt.Application.Configuration.Commands;

namespace Irt.Application.Configuration.Behaviors
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;

    public class ValidationBehavior<TRequest, TResponse>
        (IEnumerable<IValidator<TRequest>> validators,
            ICommandHandler<TRequest, TResponse> next) : ICommandHandler<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = 
                await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = 
                validationResults
                .Where(r => r.Errors.Count != 0)
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Count != 0) 
            {
                throw new ValidationException(failures);
            }

            return await next.HandleAsync(request, cancellationToken);
        }
    }
}