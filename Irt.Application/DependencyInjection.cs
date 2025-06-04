using FluentValidation;
using Irt.Application.Configuration.Commands;
using Irt.Application.Configuration.ContextAccessor;
using Irt.Application.Configuration.Emails;
using Irt.Application.Configuration.Queries;
using Irt.Application.Configuration.Validation;
using Irt.Application.Datasets;
using Irt.Application.Datasets.Commands;
using Irt.Application.Datasets.Commands.Handlers;
using Irt.Application.Datasets.Queries;
using Irt.Application.Datasource.Commands.Handlers;
using Irt.Application.Datasource.Queries;
using Irt.Application.Datasources;
using Irt.Application.Datasources.Commands;
using Irt.Application.Datasources.Queries;
using Irt.Application.Mappers;
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

        // using the <,> notation to specify the behavior that can be used for any generic type parameters
        
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddDtoValidators();

        services.AddTransient<IValidator<UpdateDatasetCommand>, UpdateDatasetCommandValidator>();
        services.AddTransient<IValidator<CreateDatasetCommand>, CreateDatasetCommandValidator>();
        
        services.AddScoped<IQueryHandler<GetDatasetsQuery, Result<List<DatasetDto>>>, GetAllDatasetsQueryHandler>();
        services
            .AddScoped<IQueryHandler<GetDatasourceQuery, Result<List<DatasourceDto>>>,
                GetDatasourceQueryHandler>();
        services.AddLogging(config => config.AddConsole()); // Console logging

        services.AddCommandHandler<CreateDatasetCommand, DatasetDto, CreateDatasetCommandHandler>();
        services.AddCommandHandler<UpdateDatasetCommand, DatasetDto, UpdateDatasetCommandHandler>();
        services.AddCommandHandler<UpdateDatasourceCommand, DatasourceDto, UpdateDatasourceCommandHandler>();
        services.AddCommandHandler<CreateDatasourceCommand, DatasourceDto, CreateDatasourceCommandHandler>();
        services.AddTransient<UpdateDatasetCommandHandler>();
        
        services.AddTransient<UpdateDatasourceCommandHandler>();
        
        services.AddFeatureManagement();
        /*services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq://localhost:5672", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(context);
                cfg.Durable = true;
            });
        });*/

        services.AddMassTransit(x =>
        {
            x.UsingInMemory();
            x.AddRider(rider =>
            {
                rider.UsingKafka((context, k) =>
                {
                    // Configure the broker
                    k.Host("localhost:9092");


                    // Configure producers
                    /*k.TopicProducer<ExampleConsumer>(c =>
                    {
                        c.TopicName = "your-topic-name";
                    });*/

                    // Configure producer
                    /*k.TopicEndpoint<ExampleConsumer>("example-topic", "producer-group", e =>
                    {
                        e.ConfigureConsumer<ExampleConsumer>(context);
                        
                    });*/
                });
                /*cfg.Host("localhost:9092");

                // Configure a consumer for a topic
                cfg.TopicEndpoint<string>("example-topic", "consumer-group", e =>
                {
                    e.ConfigureConsumer<ExampleConsumer>(context);
                });*/
            });

            // Register consumer
            //x.AddConsumer<ExampleConsumer>();
        });

        //services.AddScoped(typeof(IScopedBusContextProvider<>), typeof(ScopedBusContextProvider<>));
        //var busControl = services.BuildServiceProvider().GetRequiredService<IBusControl>();
        //busControl.StartAsync().GetAwaiter().GetResult();
        services.AddAutoMapper(typeof(DatasetMappingProfile));
        services.AddHttpContextAccessor();
        services.AddScoped<IOperationContextAccessor, OperationContextAccessor>();
        return services;
    }
}