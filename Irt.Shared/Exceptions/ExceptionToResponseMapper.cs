using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Irt.Application;
using Irt.Application.Exceptions;

namespace Irt.Shared.Exceptions
{
    public class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> Codes = new();

        public ExceptionResponse Map(Exception exception)
            => exception switch
            {
                InfrastructureException ex => new ExceptionResponse(new ErrorResponse(new Error(GetErrorCode(ex), ex.Message)), HttpStatusCode.BadRequest),
                ApplicationException ex => new ExceptionResponse(new ErrorResponse(new Error(GetErrorCode(ex), ex.Message)), HttpStatusCode.BadRequest),
                NotFoundException ex => new ExceptionResponse(new ErrorResponse(new Error(GetErrorCode(ex), ex.Message)), HttpStatusCode.NotFound),
                Application.Exceptions.ValidationException ex => new ExceptionResponse(new ErrorResponse(new Error(GetErrorCode(ex), ex.Message)), HttpStatusCode.BadRequest),
                DomainException ex => new ExceptionResponse(new ErrorResponse(new Error(GetErrorCode(ex), ex.Message)), HttpStatusCode.BadRequest),
                _ => new ExceptionResponse(new ErrorResponse(new Error(GetErrorCode(exception), exception.Message)), HttpStatusCode.InternalServerError)
            };

        private record Error(string Code, string Message);
        private record ErrorResponse(params Error[] Error);

        private static string GetErrorCode(object exception)
        {
            var type = exception.GetType();
            return Codes.GetOrAdd(type, type.Name.ToSnakeCase().Replace("_exception", string.Empty));
        }
    }
}