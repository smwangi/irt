using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Results;
using Microsoft.AspNetCore.OData.Query;

namespace Irt.SharedKernel.Extensions;

public static class ODataExtensions
{
    extension<T>(IQueryable<T> query)
    {
        public Result<IQueryable<T>> ApplyODataSafely(ODataQueryOptions? options)
        {
            if (options == null)
            {
                return Result.Success(query);
            }
            
            // Validate OData options before applying
            var validationResult = ValidateODataOptions(options);
            if (validationResult.IsFailure)
            {
                return Result.Failure<IQueryable<T>>(validationResult.IrtError);
            }

            return options.ApplyTo(query) is IQueryable<T> appliedQuery
                ? Result.Success(appliedQuery) 
                : Result.Failure<IQueryable<T>>(IrtError.BadRequest("Failed to apply OData options."));
        }
    }

    private static Result ValidateODataOptions(ODataQueryOptions? options)
    {
        if (options?.Filter?.RawValue?.Length > 1_0000)
        {
            return Result.Failure(IrtError.BadRequest("Filter query is too long."));
        }

        return options?.Top?.Value > 1_0000 
            ? Result.Failure(IrtError.BadRequest("Top value exceeds maximum allowed 1000.")) 
            : Result.Success();
    }
}
