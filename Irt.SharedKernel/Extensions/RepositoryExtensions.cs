using System.Linq.Expressions;
using AutoMapper;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.SharedKernel.Extensions;

public static class RepositoryExtensions
{
    public static async Task<Result<T>> FindByIdAsync<T>(
        this IReadOnlyRepository<T> repository,
        object id,
        CancellationToken cancellationToken = default) where T : class
    {
        var entity = await repository.FindByIdAsync(id, cancellationToken);
        return entity is not null
            ? Result<T>.Success(entity)
            : Result.Failure<T>(IrtError.NotFound($"Entity with id {id} was not found."));
    }

    public static Result<IQueryable<TDto>> QuerySafely<TEntity, TDto>(
        this IReadOnlyRepository<TEntity> repository,
        Expression<Func<TEntity, TDto>> projection) where TEntity : class
    {
        try
        {
            var query = repository.Query().Select(projection);
            return Result<IQueryable<TDto>>.Success(query);
        }
        catch (Exception e)
        {
            return Result.Failure<IQueryable<TDto>>(
                IrtError.BadRequest($"Failed to query for {typeof(TEntity).Name}. {e.Message}"));
        }
    }
    
    public static Result<IQueryable<TDto>> QuerySafely<TEntity, TDto>(
        this IReadOnlyRepository<TEntity> repository,
        Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TDto>> projection) where TEntity : class
    {
        try
        {
            var query = repository
                .Query(filter)
                .Select(projection);
            return Result<IQueryable<TDto>>.Success(query);
        }
        catch (Exception e)
        {
            return Result.Failure<IQueryable<TDto>>(
                IrtError.BadRequest($"Failed to query for {typeof(TEntity).Name}. {e.Message}"));
        }
    }
}