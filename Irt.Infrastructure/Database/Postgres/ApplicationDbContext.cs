using Irt.Core.Datasets;
using Irt.Core.Datasources;
using Irt.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Irt.Infrastructure.Database.Postgres;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Dataset> Datasets { get; set; }

    public DbSet<Datasource> Datasources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Datasource>()
            .ToTable("datasources", schema: "irt");
        modelBuilder.Entity<Datasource>()
            .HasKey(d => d.Id);
        modelBuilder.Entity<Datasource>()
            .Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => new DatasourceId(value));
        modelBuilder.Entity<Datasource>()
            .Property(p => p.CreatedBy)
            .HasConversion(
                id => id.Value,
                value => new CreatedBy(value));
        modelBuilder.Entity<Datasource>()
            .Property(p => p.LastModifiedBy)
            .HasConversion(
                id => id.Value,
                value => new LastModifiedBy(value));
        modelBuilder.Entity<Datasource>()
            .Property(p => p.Name)
            .HasConversion(
                name => name.Value,
                value => Name.Of(value));
        
        modelBuilder.Entity<Dataset>()
            .ToTable("datasets", schema: "irt");
        modelBuilder.Entity<Dataset>()
            .Property(d => d.Id)
            .HasConversion(
                id => id.Value,
                value => new DatasetId(value));
            modelBuilder.Entity<Dataset>()
                .Property(p => p.CreatedBy)
            .HasConversion(
                id => id.Value,
                value => new CreatedBy(value));
        modelBuilder.Entity<Dataset>()
            .Property(p => p.LastModifiedBy)
            .HasConversion(
                id => id.Value,
                value => new LastModifiedBy(value));
        modelBuilder.Entity<Dataset>()
            .Property(p => p.Name)
            .HasConversion(
                name => name.Value,
                value => Name.Of(value));
        
    }
    
    // To help with migration
    //public ApplicationDbContext() {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            
            var connectionString = configuration.GetConnectionString("PostgresConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}