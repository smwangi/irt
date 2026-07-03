using System.Data;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Irt.Infrastructure.Database.Postgres;

public class GenericNameUniquenessChecker<TEntity, TId>(
    ApplicationDbContext applicationDbContext) : INameUniquenessChecker<TEntity, TId>
    where TEntity : class, IEntity where TId : TypedIdValueBase<TId>
{
    public async Task<bool> IsNameUniqueAsync(string nameValue, CancellationToken cancellationToken = default)
    {
        var tableName = GetQualifiedTableName();

        var sql = $"SELECT COUNT(*) FROM {tableName} WHERE LOWER(\"Name\") = LOWER(@nameValue)";

        var connection = applicationDbContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        try
        {
            var count = await connection.ExecuteScalarAsync<int>(
                sql,
                new { nameValue },
                commandType: CommandType.Text);

            return count == 0;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public async Task<bool> IsNameUniqueAsync(string nameValue, TId excludeId, CancellationToken cancellationToken = default)
    {
        var tableName = GetQualifiedTableName();

        var sql = $"SELECT COUNT(*) FROM {tableName} WHERE LOWER(\"Name\") = LOWER(@nameValue) AND \"Id\" <> @currentId";

        var connection = applicationDbContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        try
        {
            var count = await connection.ExecuteScalarAsync<int>(
                sql,
                new { nameValue, currentId = excludeId.Value },
                commandType: CommandType.Text);

            return count == 0;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    private string GetQualifiedTableName()
    {
        var entityType = applicationDbContext.Model.FindEntityType(typeof(TEntity))
                         ?? throw new InvalidOperationException(
                             $"{typeof(TEntity).Name} is not mapped in the current DbContext.");

        var tableName = entityType.GetTableName()
                        ?? throw new InvalidOperationException(
                            $"{typeof(TEntity).Name} does not have a mapped table name.");

        var schema = entityType.GetSchema();
        return schema is null
            ? QuoteIdentifier(tableName)
            : $"{QuoteIdentifier(schema)}.{QuoteIdentifier(tableName)}";
    }

    private static string QuoteIdentifier(string identifier)
        => $"\"{identifier.Replace("\"", "\"\"")}\"";
}
