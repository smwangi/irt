using Irt.SharedKernel.ErrorHandling.Exceptions;

namespace IrtWeb.GraphQL;

public static class GraphQlResultExtensions
{
    extension<T>(Irt.SharedKernel.Results.Result<T> result)
    {
        public T ValueOrThrow()
        {
            return result.IsSuccess
                ? result.Value!
                : throw result.ErrorOrThrow().ToAppException();
        }
    }

    extension(IrtError error)
    {
        private AppException ToAppException()
            => new(error.Message, error.StatusCode, error.Code, error.Details);
    }
}
