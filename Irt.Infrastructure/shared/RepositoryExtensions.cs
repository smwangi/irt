using Irt.SharedKernel.Results;
using Microsoft.EntityFrameworkCore;
using Irt.SharedKernel.ErrorHandling.Exceptions;

namespace Irt.Infrastructure.Shared;

public static class RepositoryExtensions
{
    public static async Task<Result<T>> FindByIdAsync<T>(
        this DbSet<T> set,
        object id,
        IrtError? notFoundError = null)
        where T : class
    {
        var entity = await set
            .FindAsync(id);
        return entity is not null
            ? Result<T>.Success(entity)
            : Result<T>.Failure(
                notFoundError 
                ?? IrtError.NotFound($"{typeof(T).Name} with ID '{id}' not found"));
    }

    public static async Task<Result<T>> FirstOrDefaultAsync<T>(this IQueryable<T> query, Func<T, bool> predicate, IrtError? notFoundError = null)
        where T : class
    {
        var entity = query.FirstOrDefault(predicate);
        return entity is not null
            ? Result<T>.Success(entity)
            : Result<T>.Failure(notFoundError ?? IrtError.NotFound($"{typeof(T).Name} not found"));
    }
}