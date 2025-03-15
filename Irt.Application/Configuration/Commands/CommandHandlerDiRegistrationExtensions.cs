using Irt.Application.Configuration.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Irt.Application.Configuration.Commands;

public static class CommandHandlerDiRegistrationExtensions
{
    public static IServiceCollection AddCommandHandler<TCommand, TResponse, TError, THandler>(this IServiceCollection services)
        where THandler : class, ICommandHandler<TCommand, Result<TResponse, TError>>
        where TCommand : CommandBase<Result<TResponse, TError>>
    {
        services.AddScoped<ICommandHandler<TCommand, Result<TResponse, TError>>, THandler>();
        return services;
    }
    
    // Overload for multiple handler registrations
    public static IServiceCollection AddCommandHandlers(
        this IServiceCollection services, 
        Action<ICommandHandlerRegistrationBuilder> configure)
    {
        var builder = new CommandHandlerRegistrationBuilder(services);
        configure(builder);
        return services;
    }
}
public interface ICommandHandlerRegistrationBuilder
{
    ICommandHandlerRegistrationBuilder Add<TCommand, TResult, THandler>()
        where THandler : class, ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>;
}

public class CommandHandlerRegistrationBuilder(IServiceCollection services) : ICommandHandlerRegistrationBuilder
{
    public ICommandHandlerRegistrationBuilder Add<TCommand, TResult, THandler>()
        where THandler : class, ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        services.AddScoped<ICommandHandler<TCommand, TResult>, THandler>();
        return this;
    }
}