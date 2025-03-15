

namespace Irt.Infrastructure.Processing.InternalCommands
{
    public class InternalCommand
    {
        public required string Type { get; set; }
        public required string Data { get; set; }
        public Guid Id { get; set; }
        public DateTime? ProcessedDate { get; private set; }
    }
}