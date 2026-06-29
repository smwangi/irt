using System.Linq.Expressions;
using AutoMapper;
using Irt.SharedKernel.Common;
using Irt.SharedKernel.Results;

namespace Irt.SharedKernel.Repositories;

public interface IReadOnlyRepository<T> where T : class
{
    // Core OData Repository Interface
    IQueryable<T> Query();
    IQueryable<T> Query(Expression<Func<T, bool>> predicate);
    IQueryable<TDto> Query<TDto>(Expression<Func<T, TDto>> projection); // Enforce Projection
    IQueryable<TDto> Query<TDto>(Expression<Func<T, bool>> predicate, Expression<Func<T, TDto>> projection);
    
    // Single entity retrieval
    IQueryable<T> QueryById<TId>(TId id);
    IQueryable<TDto> QueryById<TDto, TId>(TId id, Expression<Func<T, TDto>> projection);
    
    // Include operations for eager loading (returns IQueryable)
    Task<T?> FindByIdAsync<TId>(TId id, CancellationToken cancellationToken = default);
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<TDto?> FindAsync<TDto>(Expression<Func<T, bool>> predicate, IMapper mapper, CancellationToken cancellationToken = default);
    
    // Multiple entity retrieval with projections
    Task<List<TDto>> GetAllAsync<TDto>(IMapper mapper, CancellationToken cancellationToken = default);
    Task<List<TDto>> GetAllAsync<TDto>(Expression<Func<T, bool>> predicate, IMapper mapper, CancellationToken cancellationToken = default);
    
    // Existence checks
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync<TId>(TId id, CancellationToken cancellationToken = default);
    
    // Counting
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<long> LongCountAsync(CancellationToken cancellationToken = default);
    Task<long> LongCountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    // Paging
    Task<PagedResult<T>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<PagedResult<T>> GetPagedAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<PagedResult<TDto>> GetPagedAsync<TDto>(int pageNumber, int pageSize, IMapper mapper, CancellationToken cancellationToken = default);
    Task<PagedResult<TDto>> GetPagedAsync<TDto>(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, IMapper mapper, CancellationToken cancellationToken = default);
    
    // Ordering
    Task<List<T>> GetOrderedAsync<TKey>(Expression<Func<T, TKey>> orderBy, bool ascending = true, CancellationToken cancellationToken = default);
    Task<List<TDto>> GetOrderedAsync<TDto, TKey>(Expression<Func<T, TKey>> orderBy, IMapper mapper, bool ascending = true, CancellationToken cancellationToken = default);
    
    // First/Single operations
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<TDto?> FirstOrDefaultAsync<TDto>(Expression<Func<T, bool>> predicate, IMapper mapper, CancellationToken cancellationToken = default);
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<TDto?> SingleOrDefaultAsync<TDto>(Expression<Func<T, bool>> predicate, IMapper mapper, CancellationToken cancellationToken = default);
    
    // Aggregations
    Task<TResult?> MaxAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken cancellationToken = default);
    Task<TResult?> MinAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken cancellationToken = default);
    Task<decimal> SumAsync(Expression<Func<T, decimal>> selector, CancellationToken cancellationToken = default);
    Task<double> AverageAsync(Expression<Func<T, decimal>> selector, CancellationToken cancellationToken = default);
    
    // Include operations for eager loading
    IQueryable<T> QueryWithIncludes(params Expression<Func<T, object>>[] includes);
    IQueryable<T> QueryWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    IQueryable<TDto> QueryWithIncludes<TDto>(IMapper mapper, params Expression<Func<T, object>>[] includes);
    Task<T?> FindWithIncludesAsync<TId>(TId id, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes);
    
    // Specification pattern support
    IQueryable<T> Query(ISpecification<T> specification);
    IQueryable<TDto> Query<TDto>(ISpecification<T> specification, IMapper mapper);
    Task<List<T>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    Task<PagedResult<T>> GetPagedAsync(ISpecification<T> specification, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

}
