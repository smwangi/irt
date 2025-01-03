namespace Irt.Application.Configuration.Extensions;

using Irt.Application.Exceptions;
using FluentValidation;
public static class RepositoryExtensions
{
    public static async Task<T> GetOrThrowAsync<T>(this Func<Task<T?>> fetchFunc, object id, string resourceName) where T : class
    {
        var result = await fetchFunc();
        return result ?? throw new NotFoundException($"{resourceName} with ID {id} was not found.");
    }

    public static IRuleBuilderOptions<T, string> Alphanumeric<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Matches("^[a-zA-Z0-9-]+$").WithMessage("{PropertyName} must be alphanumeric.");
    }
}