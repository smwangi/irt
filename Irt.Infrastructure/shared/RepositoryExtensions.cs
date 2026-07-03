using Irt.SharedKernel.Results;
using Microsoft.EntityFrameworkCore;
using Irt.SharedKernel.ErrorHandling.Exceptions;

namespace Irt.Infrastructure.Shared;

public static class RepositoryExtensions
{
    extension<T>(DbSet<T> set) where T : class
    {
        public async Task<Result<T>> FindByIdAsync(
            object id,
            IrtError? notFoundError = null)
        {
            var entity = await set
                .FindAsync(id);
            return entity is not null
                ? Result<T>.Success(entity)
                : Result<T>.Failure(
                    notFoundError 
                    ?? IrtError.NotFound($"{typeof(T).Name} with ID '{id}' not found"));
        }
    }

    extension<T>(IQueryable<T> query) where T : class
    {
        public async Task<Result<T>> FirstOrDefaultAsync(
            Func<T, bool> predicate,
            IrtError? notFoundError = null)
        {
            var entity = query.FirstOrDefault(predicate);
            return entity is not null
                ? Result<T>.Success(entity)
                : Result<T>.Failure(notFoundError ?? IrtError.NotFound($"{typeof(T).Name} not found"));
        }
    }
}
