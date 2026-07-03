using Irt.Application.Common;
using Irt.Core.Datasets;
using Irt.Core.Datasources;
using Irt.Core.IndicatorCategories;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.IndicatorMainCategories;
using Irt.Core.ReportingScopes;
using Irt.Core.SeedWork;
using Irt.Core.UnitOfMeasurements;
using Irt.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Irt.Infrastructure.Database.Postgres;

public class ApplicationDbContext : DbContext
{
    private readonly IDomainEventInterface.IDomainEventDispatcher _domainEventDispatcher;
    private readonly IUserDetails? _userDetails;
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDomainEventInterface.IDomainEventDispatcher domainEventDispatcher,
        IUserDetails? userDetails = null) : base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
        _userDetails = userDetails;
    }
    
    private ApplicationDbContext(){}

    public DbSet<Dataset> Datasets { get; set; }
    public DbSet<Datasource> Datasources { get; set; }
    public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
    public DbSet<IndicatorMainCategory> IndicatorMainCategories { get; set; }
    public DbSet<IndicatorCategory> IndicatorCategories { get; set; }
    public DbSet<ReportingScope> ReportingScopes { get; set; }
    public DbSet<IndicatorDefinition> IndicatorDefinitions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            /*if (typeof(ISoftDeletable).IsAssignableFrom(entity.ClrType))
            {
                modelBuilder.Entity(entity.ClrType)
                    .HasQueryFilter(e => EF.Property<bool>(e, "IsDeleted") == false);
            }*/
        }
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Datasource>()
            .ToTable("datasources", schema: "irt");
        modelBuilder.Entity<Datasource>()
            .OwnsOne(e => e.CreatedBy);
        modelBuilder.Entity<Datasource>()
            .OwnsOne(e => e.LastModifiedBy);
        
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
        modelBuilder.Entity<Datasource>().OwnsOne(e => e.CreatedBy, cb =>
        {
            cb.Property(p => p.UserId).HasColumnName("created_by_id");
            cb.Property(p => p.UserName).HasColumnName("created_by_name");
            cb.Property(p => p.Application).HasColumnName("created_by_app");
            cb.Property(p => p.IpAddress).HasColumnName("created_by_ip");
            cb.Property(p => p.Timestamp).HasColumnName("created_at");
        });

        modelBuilder.Entity<Datasource>().OwnsOne(e => e.LastModifiedBy, lb =>
        {
            lb.Property(p => p.UserId).HasColumnName("last_modified_by_id");
            lb.Property(p => p.UserName).HasColumnName("last_modified_by_name");
            lb.Property(p => p.Application).HasColumnName("last_modified_by_app");
            lb.Property(p => p.IpAddress).HasColumnName("last_modified_by_ip");
            lb.Property(p => p.Timestamp).HasColumnName("last_modified_at");
        });
        
        modelBuilder.Entity<Dataset>()
            .ToTable("datasets", schema: "irt");
        modelBuilder.Entity<Dataset>()
            .HasKey(k => k.Id);
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
        modelBuilder.Entity<Dataset>()
            .Property(prop => prop.DatasetType)
            .HasConversion(
                v => v.Value,
                v => DatasetType.From(v));
        modelBuilder.Entity<Dataset>().OwnsOne(e => e.CreatedBy, cb =>
        {
            cb.Property(p => p.UserId).HasColumnName("created_by_id");
            cb.Property(p => p.UserName).HasColumnName("created_by_name");
            cb.Property(p => p.Application).HasColumnName("created_by_app");
            cb.Property(p => p.IpAddress).HasColumnName("created_by_ip");
            cb.Property(p => p.Timestamp).HasColumnName("created_at");
        });

        modelBuilder.Entity<Dataset>().OwnsOne(e => e.LastModifiedBy, lb =>
        {
            lb.Property(p => p.UserId).HasColumnName("last_modified_by_id");
            lb.Property(p => p.UserName).HasColumnName("last_modified_by_name");
            lb.Property(p => p.Application).HasColumnName("last_modified_by_app");
            lb.Property(p => p.IpAddress).HasColumnName("last_modified_by_ip");
            lb.Property(p => p.Timestamp).HasColumnName("last_modified_at");
        });
           
           modelBuilder.Entity<IndicatorMainCategory>()
               .ToTable("indicator_main_categories", schema: "irt");
           modelBuilder.Entity<IndicatorMainCategory>()
               .HasKey(k => k.Id);
           modelBuilder.Entity<IndicatorMainCategory>()
               .Property(p => p.Id)
               .HasConversion(
                   id => id.Value,
                   value => IndicatorMainCategoryId.Create(value));
           modelBuilder.Entity<IndicatorMainCategory>()
               .Property(p => p.Name)
               .HasConversion(
                   n => n.Value,
                   value => Name.Of(value));
           modelBuilder.Entity<IndicatorMainCategory>().OwnsOne(e => e.CreatedBy, cb =>
           {
               cb.Property(p => p.UserId).HasColumnName("created_by_id");
               cb.Property(p => p.UserName).HasColumnName("created_by_name");
               cb.Property(p => p.Application).HasColumnName("created_by_app");
               cb.Property(p => p.IpAddress).HasColumnName("created_by_ip");
               cb.Property(p => p.Timestamp).HasColumnName("created_at");
           });

           modelBuilder.Entity<IndicatorMainCategory>().OwnsOne(e => e.LastModifiedBy, lb =>
           {
               lb.Property(p => p.UserId).HasColumnName("last_modified_by_id");
               lb.Property(p => p.UserName).HasColumnName("last_modified_by_name");
               lb.Property(p => p.Application).HasColumnName("last_modified_by_app");
               lb.Property(p => p.IpAddress).HasColumnName("last_modified_by_ip");
               lb.Property(p => p.Timestamp).HasColumnName("last_modified_at");
           });
           
           modelBuilder.Entity<IndicatorCategory>()
               .ToTable("indicator_categories", schema: "irt");
           modelBuilder.Entity<IndicatorCategory>()
               .HasKey(k => k.Id);
           modelBuilder.Entity<IndicatorCategory>()
               .Property(p => p.Id)
               .HasConversion(
                   id => id.Value,
                   value => IndicatorCategoryId.Create(value));
            modelBuilder.Entity<IndicatorCategory>()
                .Property(p => p.Name)
                .HasConversion(
                    n => n.Value,
                    value => Name.Of(value));
            modelBuilder.Entity<IndicatorCategory>().OwnsOne(e => e.CreatedBy, cb =>
            {
                cb.Property(p => p.UserId).HasColumnName("created_by_id");
                cb.Property(p => p.UserName).HasColumnName("created_by_name");
                cb.Property(p => p.Application).HasColumnName("created_by_app");
                cb.Property(p => p.IpAddress).HasColumnName("created_by_ip");
                cb.Property(p => p.Timestamp).HasColumnName("created_at");
            });

            modelBuilder.Entity<IndicatorCategory>().OwnsOne(e => e.LastModifiedBy, lb =>
            {
                lb.Property(p => p.UserId).HasColumnName("last_modified_by_id");
                lb.Property(p => p.UserName).HasColumnName("last_modified_by_name");
                lb.Property(p => p.Application).HasColumnName("last_modified_by_app");
                lb.Property(p => p.IpAddress).HasColumnName("last_modified_by_ip");
                lb.Property(p => p.Timestamp).HasColumnName("last_modified_at");
            });
           
           modelBuilder.Entity<IndicatorDefinition>()
               .ToTable("indicator_definitions", schema: "irt");
           modelBuilder.Entity<IndicatorDefinition>()
               .OwnsOne(idn => idn.CreatedBy);
           modelBuilder.Entity<IndicatorDefinition>()
               .OwnsOne(idn => idn.LastModifiedBy);
           modelBuilder.Entity<IndicatorDefinition>()
               .HasKey(k => k.Id);
           modelBuilder.Entity<IndicatorDefinition>()
               .Property(p => p.Id)
               .HasConversion(
                   id => id.Value,
                   value => IndicatorDefinitionId.Create(value))
               .HasColumnName("indicator_definition_name");
           modelBuilder.Entity<IndicatorDefinition>()
               .Property(p => p.Name)
               .HasConversion(
                   n => n.Value,
                   value => Name.Of(value));
           modelBuilder.Entity<IndicatorDefinition>().OwnsOne(e => e.CreatedBy, cb =>
           {
               cb.Property(p => p.UserId).HasColumnName("created_by_id");
               cb.Property(p => p.UserName).HasColumnName("created_by_name");
               cb.Property(p => p.Application).HasColumnName("created_by_app");
               cb.Property(p => p.IpAddress).HasColumnName("created_by_ip");
               cb.Property(p => p.Timestamp).HasColumnName("created_at");
           });

           modelBuilder.Entity<IndicatorDefinition>().OwnsOne(e => e.LastModifiedBy, lb =>
           {
               lb.Property(p => p.UserId).HasColumnName("last_modified_by_id");
               lb.Property(p => p.UserName).HasColumnName("last_modified_by_name");
               lb.Property(p => p.Application).HasColumnName("last_modified_by_app");
               lb.Property(p => p.IpAddress).HasColumnName("last_modified_by_ip");
               lb.Property(p => p.Timestamp).HasColumnName("last_modified_at");
           });
           
           modelBuilder.Entity<ReportingScope>()
               .ToTable("reporting_scopes", schema: "irt");
           modelBuilder.Entity<ReportingScope>()
               .HasKey(k => k.Id);
           modelBuilder.Entity<ReportingScope>()
               .Property(p => p.Id)
               .HasConversion(
                   id => id.Value,
                   value => ReportingScopeId.Create(value));
           modelBuilder.Entity<ReportingScope>()
               .Property(p => p.Name)
               .HasConversion(
                   n => n.Value,
                   value => Name.Of(value));
           modelBuilder.Entity<ReportingScope>().OwnsOne(e => e.CreatedBy, cb =>
           {
               cb.Property(p => p.UserId).HasColumnName("created_by_id");
               cb.Property(p => p.UserName).HasColumnName("created_by_name");
               cb.Property(p => p.Application).HasColumnName("created_by_app");
               cb.Property(p => p.IpAddress).HasColumnName("created_by_ip");
               cb.Property(p => p.Timestamp).HasColumnName("created_at");
           });

           modelBuilder.Entity<ReportingScope>().OwnsOne(e => e.LastModifiedBy, lb =>
           {
               lb.Property(p => p.UserId).HasColumnName("last_modified_by_id");
               lb.Property(p => p.UserName).HasColumnName("last_modified_by_name");
               lb.Property(p => p.Application).HasColumnName("last_modified_by_app");
               lb.Property(p => p.IpAddress).HasColumnName("last_modified_by_ip");
               lb.Property(p => p.Timestamp).HasColumnName("last_modified_at");
           });
           
           modelBuilder.Entity<UnitOfMeasure>()
               .ToTable("unit_of_measures", schema: "irt");
           modelBuilder.Entity<UnitOfMeasure>()
               .HasKey(k => k.Id);
           modelBuilder.Entity<UnitOfMeasure>()
               .Property(p => p.Id)
               .HasConversion(
                   id => id.Value,
                   value => UnitOfMeasureId.Create(value));
           modelBuilder.Entity<UnitOfMeasure>()
               .Property(p => p.Name)
               .HasConversion(
                   n => n.Value,
                   value => Name.Of(value));
           modelBuilder.Entity<UnitOfMeasure>().OwnsOne(e => e.CreatedBy, cb =>
           {
               cb.Property(p => p.UserId).HasColumnName("created_by_id");
               cb.Property(p => p.UserName).HasColumnName("created_by_name");
               cb.Property(p => p.Application).HasColumnName("created_by_app");
               cb.Property(p => p.IpAddress).HasColumnName("created_by_ip");
               cb.Property(p => p.Timestamp).HasColumnName("created_at");
           });

           modelBuilder.Entity<UnitOfMeasure>().OwnsOne(e => e.LastModifiedBy, lb =>
           {
               lb.Property(p => p.UserId).HasColumnName("last_modified_by_id");
               lb.Property(p => p.UserName).HasColumnName("last_modified_by_name");
               lb.Property(p => p.Application).HasColumnName("last_modified_by_app");
               lb.Property(p => p.IpAddress).HasColumnName("last_modified_by_ip");
               lb.Property(p => p.Timestamp).HasColumnName("last_modified_at");
           });

    }
    

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
        ApplyAuditInformation();

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

    private void ApplyAuditInformation()
    {
        var userId = _userDetails?.UserId ?? "system";
        var userName = _userDetails?.UserName ?? "system";
        var application = _userDetails?.Application ?? "system";
        var ipAddress = _userDetails?.IpAddress ?? "127.0.0.1";
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<IEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetCreatedByInfo(
                        CreatedBy.Create(userId, userName, application, now, ipAddress));
                    entry.Entity.SetLastModifiedByInfo(
                        LastModifiedBy.Create(userId, userName, application, now, ipAddress));
                    break;
                case EntityState.Modified:
                    entry.Entity.SetLastModifiedByInfo(
                        LastModifiedBy.Create(userId, userName, application, now, ipAddress));
                    break;
            }
        }
    }
}
