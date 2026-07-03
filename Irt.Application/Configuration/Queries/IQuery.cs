
using Microsoft.AspNetCore.OData.Query;

namespace Irt.Application.Configuration.Queries
{
    public interface IQuery<out TResult>
    {
    }
    
    public interface IODataQuery<T> : IQuery<T>
    {
        ODataQueryOptions? Options { get; set; }
    }

    public abstract class BaseODataQuery<TResult> : IODataQuery<TResult>
    {
        public ODataQueryOptions? Options { get; set; }
    }
}