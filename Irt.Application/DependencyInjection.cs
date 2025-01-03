using FluentValidation;
using Irt.Application.Behaviors;
using Irt.Application.Configuration.Commands;
using Irt.Application.Configuration.Emails;
using Irt.Application.Configuration.Notifications;
using Irt.Application.Configuration.Results;
using Irt.Application.Datasets;
using Irt.Application.Datasets.Commands;
using Irt.Application.Datasets.Commands.Handlers;
using Irt.Application.Datasources;
using Irt.Application.Datasources.Commands;
using Irt.Application.Datasources.Commands.Handlers;
using Irt.Application.Mappers;
using Irt.Core.Datasets;
using Irt.Core.Datasources;
using Irt.Core.IndicatorDefinitions;
using Irt.Core.SharedKernel;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace Irt.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        // command handlers
        
        //services.AddScoped<IDatasourceUniqueChecker, DatasourceUniqueChecker>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        // using the <,> notation to specify the behavior that can be used for any generic type parameters
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<INotification, DatasourceAddedNotification>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<INotificationHandler<DatasourceAddedNotification>>(x => new EmailHandler(x.GetService<IEmailSender>()!));

        services.AddScoped<ICommandHandler<UpdateDatasetCommand, UpdateResult<string>>, UpdateDatasetCommandHandler>();
        services.AddScoped<ICommandHandler<CreateDatasetCommand, Result<DatasetDto, string>>, CreateDatasetCommandHandler>();

        services.AddScoped<ICommandHandler<UpdateDatasourceCommand, UpdateResult<string>>>(
            x =>
            new UpdateDatasourceCommandHandler(
                x.GetService<IDatasourceRepository>()!,
                x.GetService<INameValidationChecker<Datasource>>()!));
        services.AddScoped<ICommandHandler<CreateDatasourceCommand, Result<DatasourceDto, string>>>(
            x =>
            new CreateDatasourceCommandHandler(
                x.GetService<IDatasourceRepository>()!,
                x.GetService<INameValidationChecker<Datasource>>()!));

        services.AddTransient<IValidator<UpdateDatasetCommand>, UpdateDatasetCommandValidator>();

        services.AddScoped<INameValidationChecker<Datasource>, DatasourceNameValidationChecker>();
        services.AddScoped<INameValidationChecker<Dataset>, DatasetNameValidationChecker>();

        /*services.AddTransient(typeof(IRequestHandler<,>), typeof(UpdateDatasetCommandHandler));
        services.AddTransient(typeof(IRequestHandler<,>), typeof(CreateDatasetCommandHandler));
        services.AddTransient(typeof(IRequestHandler<,>), typeof(UpdateDatasourceCommandHandler));
        services.AddTransient(typeof(IRequestHandler<,>), typeof(CreateDatasourceCommandHandler));*/
        
        //services.AddScoped<ValidationBehavior<,>, IPipelineBehavior<,>>();
        services.AddFeatureManagement();
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq://localhost");
            });
        });

        services.AddAutoMapper(typeof(DatasetMappingProfile));
        return services;
    }
}