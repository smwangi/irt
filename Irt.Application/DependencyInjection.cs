using FluentValidation;
using Irt.Application.Common;
using Irt.Application.Configuration.Behaviors;
using Irt.Application.Configuration.Commands;
using Irt.Application.Configuration.ContextAccessor;
using Irt.Application.Configuration.Queries;
using Irt.Application.Datasets;
using Irt.Application.Datasets.Queries;
using Irt.Application.Datasource;
using Irt.Application.Datasource.Queries;
using Irt.Application.Mappers;
using Irt.Application.ReportingScopes;
using Irt.Application.ReportingScopes.Queries;
using Irt.Application.ReportingScopes.Queries.Handlers;
using Irt.Core.Datasets;
using Irt.Core.ReportingScopes;
using Irt.SharedKernel.Repositories;
using Irt.SharedKernel.Results;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace Irt.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped<IQueryableQueryHandler<GetDatasetsQuery, Result<IQueryable<DatasetDto>>>, GetAllDatasetsQueryHandler>();
        
        //services.AddScoped<IQueryableQueryHandler<GetDatasourceQuery, DatasourceDto>, GetDatasourceQueryHandler>();
        //services.AddScoped<IQueryableQueryHandler<GetReportingScopeQuery, ReportingScopeDto>, GetReportingScopeQueryHandler>();
        //services.AddScoped<IQueryHandler<GetDatasetsByIdQuery, DatasetDto>, GetDatasetsByIdQueryHandler>();
        
        services.AddLogging(config => config.AddConsole()); // Console logging

        services.AddHttpContextAccessor();
        services.AddScoped<IUserDetails, HttpContextUserDetails>();
        services.Scan(scan => scan
            .FromApplicationDependencies()
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Decorate(typeof(ICommandHandler<,>), typeof(UnitOfWorkCommandHandlerDecorator<,>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationBehavior<,>));

        services.Scan(scan => scan
            .FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IProjection<,>))
            .AddClasses(classes => classes.AssignableTo(typeof(IProjection<,>)))
            .AsImplementedInterfaces()
        .WithScopedLifetime());
        
        services.AddFeatureManagement();
        services.AddMassTransit(x =>
        {
            x.UsingInMemory();
            x.AddRider(rider =>
            {
                rider.UsingKafka((context, k) =>
                {
                    // Configure the broker
                    k.Host("localhost:9092");


                });
            });
        });

        services.AddAutoMapper(typeof(DatasetMappingProfile));
        services.AddHttpContextAccessor();
        services.AddScoped<IOperationContextAccessor, OperationContextAccessor>();
        return services;
    }
}
