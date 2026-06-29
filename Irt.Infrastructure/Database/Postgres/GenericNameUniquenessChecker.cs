using System.Data;
using System.Linq.Expressions;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Irt.Infrastructure.Database.Postgres;

    public class GenericNameUniquenessChecker<TEntity, TId>(
        ApplicationDbContext applicationDbContext) :  INameUniquenessChecker<TEntity, TId>
    where TEntity : class, IEntity where TId : TypedIdValueBase<TId>
    {
        public async Task<bool> IsNameUniqueAsync(string nameValue, CancellationToken cancellationToken = default)
        {
            // Get the table name
            var tableName = (applicationDbContext.Model.FindEntityType(typeof(TEntity)) ?? throw new InvalidOperationException()).GetTableName();

            // Construct the SQL query
            var sql = $"SELECT COUNT(*) FROM {tableName} WHERE LOWER(\"Name\") = LOWER(@nameValue)";

            // Open the connection
            var connection = applicationDbContext.Database.GetDbConnection();
            await connection.OpenAsync(cancellationToken);

            try 
            {
                // Execute the query using Dapper
                var count = await connection.ExecuteScalarAsync<int>(
                    sql, 
                    new { nameValue }, 
                    commandType: CommandType.Text
                );

                // Return true if count is 0 (unique), false otherwise
                return count == 0;
            }
            finally 
            {
                await connection.CloseAsync();
            }
        }

    public async Task<bool> IsNameUniqueAsync(string nameValue, TId excludeId, CancellationToken cancellationToken = default)
    {
        // Get the table name
        var tableName = (applicationDbContext.Model.FindEntityType(typeof(TEntity)) ?? throw new InvalidOperationException()).GetTableName();

        // Construct the SQL query
        var sql = $"SELECT COUNT(*) FROM {tableName} WHERE LOWER(\"Name\") = LOWER(@nameValue) AND \"Id\" <> @currentId";

        // Open the connection
        var connection = applicationDbContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        try 
        {
            // Execute the query using Dapper
            var count = await connection.ExecuteScalarAsync<int>(
                sql, 
                new { nameValue, currentId = excludeId.Value }, 
                commandType: CommandType.Text
            );

            // Return true if count is 0 (unique), false otherwise
            return count == 0;
        }
        finally 
        {
            await connection.CloseAsync();
        }
    }
}
