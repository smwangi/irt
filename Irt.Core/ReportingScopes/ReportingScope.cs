using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;

namespace Irt.Core.ReportingScopes
{
    public class ReportingScope : Entity<ReportingScopeId>
    {
        public string Description { get; private set; }
        public Name ReportingScopeName { get; private set; }

        private ReportingScope(ReportingScopeId id, Name name, string description) : base(id)
        {
            Id = id;
            ReportingScopeName = name;
            Description = description;
        }

        public static ReportingScope CreateReportingScope(
            Name name,
            string description)
        {
            return new ReportingScope(
                new ReportingScopeId(UniqueIdGenerator.NextId()),
                name,
                description);
        }
    }
}