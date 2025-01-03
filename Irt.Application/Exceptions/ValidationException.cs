using FluentValidation.Results;

namespace Irt.Application.Exceptions
{
    public class ValidationException(IEnumerable<ValidationFailure> failures) : Exception(string.Join(", ", failures))
    {
    }
}