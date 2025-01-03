namespace Irt.Application.Helpers;

using Irt.Application.Exceptions;
public static class RepositoryHelpers
{
    public static async Task<T?> GetOrThrowAsync<T>(Func<Task<T?>> fetchFunc, object id, string resourceName) where T : class
    {
        var result = await fetchFunc();
        return result ?? throw new NotFoundException($"{resourceName} with ID {id} was not found.");
    }
}