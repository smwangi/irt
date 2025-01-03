using System;
using MediatR;
using MongoDB.Bson;

namespace Irt.Core.SeedWork
{   
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn  => DateTime.UtcNow;
        string EventType => GetType().AssemblyQualifiedName!;
        string EventId => Guid.NewGuid().ToString();
        string Id => ObjectId.GenerateNewId().ToString();
    }
}