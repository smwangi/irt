using Irt.Application.Exceptions;

namespace Irt.Shared.Exceptions
{
    public interface IExceptionCompositionRoot
    {
        ExceptionResponse Map(Exception exception);
    }
}