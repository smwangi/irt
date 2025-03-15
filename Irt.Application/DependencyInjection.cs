using FluentValidation;
using Irt.Application.Behaviors;
using Irt.Application.Configuration.Behaviors;
using Irt.Application.Configuration.Commands;
using Irt.Application.Configuration.Emails;
using Irt.Application.Configuration.Queries;
using Irt.Application.Configuration.Results;
using Irt.Application.Datasets;
using Irt.Application.Datasets.Commands;
using Irt.Application.Datasets.Commands.Handlers;
using Irt.Application.Datasets.Queries;
using Irt.Application.Datasources;
using Irt.Application.Datasources.Commands;
using Irt.Application.Datasources.Commands.Handlers;
using Irt.Application.Datasources.Queries;
using Irt.Application.Mappers;
using Irt.Core.Datasources;
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
        /*services.AddScoped<ICommandHandler<UpdateDatasetCommand, UpdateResult<string>>, UpdateDatasetCommandHandler>();
        services.AddScoped<ICommandHandler<CreateDatasetCommand, Result<DatasetDto, string>>, CreateDatasetCommandHandler>();

        services.AddScoped<ICommandHandler<UpdateDatasourceCommand, UpdateResult<string>>>(
            x =>
            new UpdateDatasourceCommandHandler(
                x.GetService<IDatasourceRepository>()!));
        services.AddScoped<ICommandHandler<CreateDatasourceCommand, Result<DatasourceDto, string>>>(
            x =>
            new CreateDatasourceCommandHandler(
                x.GetService<IDatasourceRepository>()!));*/

        services.AddTransient<IValidator<UpdateDatasetCommand>, UpdateDatasetCommandValidator>();
        services.AddTransient<IValidator<CreateDatasetCommand>, CreateDatasetCommandValidator>();
        
        // Validation pipeline
        //services.AddScoped(typeof(ICommandHandler<,>), typeof(ValidationBehavior<,>));
        // Logging behavior
        //services.AddScoped(typeof(ICommandHandler<,>), typeof(LoggingBehavior<,>));
        //services.AddScoped<ICommandHandler<CreateDatasetCommand, Result<DatasetDto, string>>, CreateDatasetCommandHandler>();
        //services.AddScoped<ICommandHandler<UpdateDatasetCommand, UpdateResult<string>>, UpdateDatasetCommandHandler>();

        services.AddScoped<IQueryHandler<GetDatasetsQuery, Result<List<DatasetDto>, string>>, GetAllDatasetsQueryHandler>();
        services
            .AddScoped<IQueryHandler<GetDatasourcesQuery, Result<List<DatasourceDto>, string>>,
                GetDatasourcesQueryHandler>();
        services.AddLogging(config => config.AddConsole()); // Console logging

        services.AddCommandHandler<CreateDatasetCommand, DatasetDto, string, CreateDatasetCommandHandler>();
        services.AddCommandHandler<UpdateDatasetCommand,DatasetDto, string, UpdateDatasetCommandHandler>();
        services.AddCommandHandler<UpdateDatasourceCommand, DatasourceDto, string, UpdateDatasourceCommandHandler>();
        services.AddCommandHandler<CreateDatasourceCommand, DatasourceDto, string, CreateDatasourceCommandHandler>();
        services.AddTransient<UpdateDatasetCommandHandler>();
        
        services.AddTransient<UpdateDatasourceCommandHandler>();
        
        //services.AddTransient<CreateDatasourceCommandHandler>();
        //services.AddTransient(typeof(ICommand<>), typeof(CommandBase<>));

        //services.AddScoped<ValidationBehavior<,>, IPipelineBehavior<,>>();
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
        return services;
    }
}