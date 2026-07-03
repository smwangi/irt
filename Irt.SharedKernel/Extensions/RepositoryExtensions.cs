using System.Linq.Expressions;
using AutoMapper;
using Irt.SharedKernel.ErrorHandling.Exceptions;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;

namespace Irt.SharedKernel.Extensions;

public static class RepositoryExtensions
{
    extension<T>(IReadOnlyRepository<T> repository) where T : class
    {
        public async Task<Result<T>> FindByIdAsync(
            object id,
            CancellationToken cancellationToken = default)
        {
            var entity = await repository.FindByIdAsync(id, cancellationToken);
            return entity is not null
                ? Result<T>.Success(entity)
                : Result.Failure<T>(IrtError.NotFound($"Entity with id {id} was not found."));
        }

        public Result<IQueryable<TDto>> QuerySafely<TDto>(
            Expression<Func<T, TDto>> projection)
        {
            try
            {
                var query = repository.Query().Select(projection);
                return Result<IQueryable<TDto>>.Success(query);
            }
            catch (Exception e)
            {
                return Result.Failure<IQueryable<TDto>>(
                    IrtError.BadRequest($"Failed to query for {typeof(T).Name}. {e.Message}"));
            }
        }
        
        public Result<IQueryable<TDto>> QuerySafely<TDto>(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, TDto>> projection)
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
                    IrtError.BadRequest($"Failed to query for {typeof(T).Name}. {e.Message}"));
            }
        }
    }
}
