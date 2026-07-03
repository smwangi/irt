using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Irt.Application.Common;

public static class QueryableSearchExtensions
{
    private static readonly MethodInfo LikeMethod = typeof(DbFunctionsExtensions)
        .GetMethod(
            nameof(DbFunctionsExtensions.Like),
            [typeof(DbFunctions), typeof(string), typeof(string)])!;

    private static readonly MethodInfo ToLowerMethod =
        typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes)!;

    private static readonly PropertyInfo FunctionsProperty =
        typeof(EF).GetProperty(nameof(EF.Functions))!;

    extension<T>(IQueryable<T> source)
    {
        /// <summary>
        /// Applies a case-insensitive, SQL-translated substring filter on the projected string.
        /// LIKE wildcards in <paramref name="search"/> are escaped so user input is treated literally.
        /// Returns the source unchanged when <paramref name="search"/> is null or whitespace.
        /// </summary>
        public IQueryable<T> WhereContainsInsensitive(
            string? search,
            Expression<Func<T, string>> selector)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return source;
            }

            var pattern = $"%{EscapeLikePattern(search.Trim().ToLower())}%";

            var toLowered = Expression.Call(selector.Body, ToLowerMethod);
            var functions = Expression.Property(null, FunctionsProperty);
            var likeCall = Expression.Call(
                LikeMethod,
                functions,
                toLowered,
                Expression.Constant(pattern));

            var predicate = Expression.Lambda<Func<T, bool>>(likeCall, selector.Parameters);
            return source.Where(predicate);
        }
    }

    private static string EscapeLikePattern(string value)
        => value
            .Replace(@"\", @"\\")
            .Replace("%", @"\%")
            .Replace("_", @"\_");
}
