using System.Reflection;
using Irt.Application.Helpers;
using Irt.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Irt.Infrastructure.Database.Postgres;

public class EntityRepository(ApplicationDbContext dbContext) : IEntityRepository
{
    public async Task<IEntity> FindEntityByIdAndType(string id, string typeName, CancellationToken cancellationToken = default)
    {
        // Get the entity type from the context's model
        var entityClrType = dbContext.Model.GetEntityTypes()
            .FirstOrDefault(t => t.ClrType.Name == typeName)?.ClrType;

        if (entityClrType == null)
        {
            throw new ArgumentException($"Entity type '{typeName}' not found in the database model.");
        }

        // Get the ID property info
        var idProperty = entityClrType.GetProperty("Id");
        if (idProperty == null)
        {
            throw new ArgumentException($"Entity type '{typeName}' does not have an Id property.");
        }

        // Get the ID's type and convert the string ID to that type
        var idType = idProperty.PropertyType;
        // Find the generic "For" method on the ID type
        var forMethod = idType.GetMethod("Create", BindingFlags.Public | BindingFlags.Static);
        if (forMethod == null)
        {
            throw new InvalidOperationException($"TypedId '{idType.Name}' does not have a static 'Create' method.");
        }
        
        // Convert the string ID to the typed ID
        var typedId = forMethod.Invoke(null, [id]);

        // Create a generic method to call DbSet<T>.FindAsync
        var dbSetMethod = typeof(DbContext).GetMethod("Set", []).MakeGenericMethod(entityClrType);
        var dbSet = dbSetMethod.Invoke(dbContext, null);

        /*var findMethod = dbSet.GetType().GetMethod("FindAsync", new[] { typeof(object[]), typeof(CancellationToken) });
        var task = (Task)findMethod.Invoke(dbSet, new object[] { new[] { typedId }, cancellationToken });

        await task.ConfigureAwait(false);*/
        
        // Get the result from the task
        // Get the result from the task
        //var resultProperty = task.GetType().GetProperty("Result");
        //return (IEntity)resultProperty.GetValue(task);
        var findMethod = dbSet.GetType().GetMethod("FindAsync", new[] { typeof(object[]), typeof(CancellationToken) });
        dynamic result = findMethod.Invoke(dbSet, [new[] { typedId }, cancellationToken]);

        // Get the result from the ValueTask<Datasource>
        var entity = await result;

        // Now you can cast to IEntity if needed
        var entityResult = (IEntity)entity;
        return entityResult;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await dbContext.SaveChangesAsync(cancellationToken);
        return result;
    }
}
