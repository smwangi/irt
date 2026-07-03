using System.Net;

namespace Irt.SharedKernel.ErrorHandling.Exceptions
{
    public record ExceptionResponse(object Response, HttpStatusCode StatusCode);
}