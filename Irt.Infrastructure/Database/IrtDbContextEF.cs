using Irt.Core.Datasources;
using Irt.Core.ValueObjects;
using Irt.Infrastructure.Processing.InternalCommands;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Irt.Infrastructure.Database
{
    public class IrtDbContextEf(DbContextOptions<IrtDbContextEf> options) : DbContext(options)
    {
        public DbSet<InternalCommand> InternalCommands { get; init; }

        public DbSet<Datasource> Datasources { get; init; }

        /*public static IrtDbContextEf Create(IMongoDatabase database) => 
            new(new DbContextOptionsBuilder<IrtDbContextEf>()
                .LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging()
                .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
                .Options);*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Datasource>(entity =>
            {
                entity.ToCollection("datasources");
                entity.HasKey(e => e.Id);
                entity.ComplexProperty(e => e.Id).Property(e => e.Value).HasConversion<string>();
                entity.ComplexProperty(e => e.Name).Property(e => e.Value).HasConversion<string>();
                /*entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name.Value).IsUnique();
                entity.OwnsOne(e => e.Id).Ignore(e => e.Value);
                entity.Property(e => e.Id).HasConversion(
                    v => v.Value,
                    v => new DatasourceId(v));
                
                entity.Property(e => e.Name).HasConversion(
                    v => v.Value,
                    v => new DatasourceName(v));
                entity.OwnsOne(e => e.Name).Property(e => e.Value);*/
            });
        }
    }
}