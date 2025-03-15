using FluentValidation;
using Irt.Application.Datasources;
using Irt.Application.Datasources.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Irt.Application.Configuration.Validation;

public static class DtoValidationDI
{
    public static IServiceCollection AddDtoValidators(this IServiceCollection services)
    {
        // Register all validators in the assembly
        services.AddScoped<IValidator<DatasourceDto>, DatasourceDtoValidator>();

        // Register specific validators if needed
        // services.AddTransient<IValidator<YourDto>, YourDtoValidator>();

        return services;
    }
}