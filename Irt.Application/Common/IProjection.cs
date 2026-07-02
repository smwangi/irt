using System.Linq.Expressions;

namespace Irt.Application.Common;

public interface IProjection<TEntity, TDto>
{
    Expression<Func<TEntity, TDto>> Expression { get; }
}