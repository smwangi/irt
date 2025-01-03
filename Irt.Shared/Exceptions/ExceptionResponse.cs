using System.Net;

namespace Irt.Application.Exceptions
{
    public record ExceptionResponse(object Response, HttpStatusCode StatusCode);
}