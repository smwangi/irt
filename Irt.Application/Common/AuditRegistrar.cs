using Irt.Core.SeedWork;
using Microsoft.Extensions.DependencyInjection;

namespace Irt.Application.Common;

public static class AuditRegistrar
{
    public static void RegisterAuditFromCommand<TId>(
        Entity<TId> entity,
        IRequireMetadata command) where TId : TypedIdValueBase<TId>
    {
        entity.RegisterCreation(
            command.UserId!,
            command.UserName!,
            command.Application!,
            command.IpAddress!);
        
        entity.RegisterModification(
            command.UserId!,
            command.UserName!,
            command.Application!,
            command.IpAddress!);
    }
    
    public static void RegisterCreationOnly<TId>(
        Entity<TId> entity,
        IRequireMetadata command) where TId : TypedIdValueBase<TId>
    {
        entity.RegisterCreation(command.UserId!, command.UserName!, command.Application!, command.IpAddress!);
    }

    public static void RegisterModificationOnly<TId>(
        Entity<TId> entity,
        IRequireMetadata command) where TId : TypedIdValueBase<TId>
    {
        entity.RegisterModification(command.UserId!, command.UserName!, command.Application!, command.IpAddress!);
    }
}