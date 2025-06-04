using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;

namespace Irt.Core.ReportingScopes
{
    public class ReportingScope : Entity<ReportingScopeId>
    {
        public string Description { get; private set; }

        private ReportingScope()
        {
        }

        private ReportingScope(ReportingScopeId id, Name name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public static ReportingScope CreateReportingScope(
            Name name,
            string description)
        {
            return new ReportingScope(
                ReportingScopeId.Create(UniqueIdGenerator.NextId()),
                name,
                description);
        }
    }
}