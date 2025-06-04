using Irt.Core.Datasets;
using Irt.Core.Datasources;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Infrastructure.Shared;
using Irt.SharedKernel.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Irt.Infrastructure.Database.Postgres;

public static class RegisterRepositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGenericRepository<Dataset>, GenericRepository<Dataset>>();
        services.AddScoped<IGenericRepository<Datasource>, GenericRepository<Datasource>>();
        services.AddScoped<IGenericRepository<IndicatorDefinition>, GenericRepository<IndicatorDefinition>>();

        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}