
using Irt.Application.Configuration.DomainEvents;
using Irt.Application.Dispatchers;
using Irt.Application.Helpers;
using Irt.Core.Datasources;
using Irt.Core.ReportingScopes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Infrastructure.Database.Postgres;
using Irt.Infrastructure.Shared;
using Irt.SharedKernel.Repositories;
using Microsoft.EntityFrameworkCore;
using DomainEvents_DomainEventsDispatcher = Irt.Application.Configuration.DomainEvents.DomainEventsDispatcher;


namespace Irt.Infrastructure;

public static class DependencyInjection
{
    

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDispatchers();
        services.AddRepositories();
        
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));
        services.AddScoped(typeof(IEntityRepository), typeof(EntityRepository));
        services.AddScoped<IDomainEventInterface.IDomainEventDispatcher, DomainEvents_DomainEventsDispatcher>();
        // Audit (CreatedBy/LastModifiedBy) is centrally applied by ApplicationDbContext.SaveChangesAsync
        // via IUserDetails, so the legacy EntityCreated/Modified event handlers are intentionally
        // not registered to avoid clobbering audit values with stale ctor-time data.
        
        services.AddScoped<INameUniquenessChecker<Datasource, DatasourceId>>(sp =>
            new GenericNameUniquenessChecker<Datasource, DatasourceId>(
                sp.GetRequiredService<ApplicationDbContext>()));

        services.AddScoped<INameUniquenessChecker<ReportingScope, ReportingScopeId>>(sp =>
            new GenericNameUniquenessChecker<ReportingScope, ReportingScopeId>(
                sp.GetRequiredService<ApplicationDbContext>()));
        
        return services;
    }
}
