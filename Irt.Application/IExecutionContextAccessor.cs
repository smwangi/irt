namespace Irt.Application;

public interface IExecutionContextAccessor
{
    Guid CorrelationId { get; }
    bool IsAvailable { get; }
}
