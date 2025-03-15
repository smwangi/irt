using Irt.Core.Datasets;
using Irt.Core.Datasources;
using Irt.Core.SeedWork;
using Irt.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Irt.Infrastructure.Database.Postgres;

public class ApplicationDbContext : DbContext
{
    private readonly IDomainEventInterface.IDomainEventDispatcher _domainEventDispatcher;
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDomainEventInterface.IDomainEventDispatcher domainEventDispatcher) : base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }
    
    private ApplicationDbContext(){}

    public DbSet<Dataset> Datasets { get; set; }

    public DbSet<Datasource> Datasources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Datasource>()
            .ToTable("datasources", schema: "irt");
        modelBuilder.Entity<Datasource>().OwnsOne(e => e.CreatedBy, cb =>
        {
            cb.WithOwner();
            cb.Property(p => p.UserId).HasColumnName("created_by_id");
            cb.Property(p => p.UserName).HasColumnName("created_by_name");
            cb.Property(p => p.CreatedAt).HasColumnName("created_at");
            cb.Property(p => p.IpAddress).HasColumnName("created_by_ip");
        });
        modelBuilder.Entity<Datasource>().OwnsOne(e => e.LastModifiedBy, lb =>
        {
            
            lb.WithOwner();
            lb.Property(p => p.UserId).HasColumnName("last_modified_by_id");
            lb.Property(p => p.UserName).HasColumnName("last_modified_by_name");
            lb.Property(p => p.ModifiedAt).HasColumnName("last_modified_at");
            lb.Property(p => p.IpAddress).HasColumnName("last_modified_by_ip");
        });
        
        modelBuilder.Entity<Datasource>()
            .HasKey(d => d.Id);
        modelBuilder.Entity<Datasource>()
            .Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => DatasourceId.Create(value));
        
        modelBuilder.Entity<Datasource>()
            .Property(p => p.Name)
            .HasConversion(
                name => name.Value,
                value => Name.Of(value));
        
        modelBuilder.Entity<Dataset>()
            .ToTable("datasets", schema: "irt");
        modelBuilder.Entity<Dataset>().OwnsOne(e => e.CreatedBy);
        modelBuilder.Entity<Dataset>().OwnsOne(e => e.LastModifiedBy);
        modelBuilder.Entity<Dataset>()
            .Property(d => d.Id)
            .HasConversion(
                id => id.Value,
                value => new DatasetId(value));
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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Get all entities that implement HasDomainEvents and has pending changes
        var entities = ChangeTracker.Entries<IHasDomainEvents>()
            //.Where(e => e.Entity.DomainEvents.Any() && e.State == EntityState.Modified)
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();
        
        // first save changes to get proper ids for new entities
        var result = await base.SaveChangesAsync(cancellationToken);
        
        // Dispatch domain events
        await _domainEventDispatcher.DispatchEventsAsync(
            entities.Cast<IEntity>().ToArray(),
            cancellationToken);

        return result;
    }
}