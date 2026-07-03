using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;

namespace Irt.Core.ReportingScopes
{
    public class ReportingScope : NamedMetadataEntity<ReportingScopeId>
    {
        private ReportingScope()
        {
        }

        private ReportingScope(ReportingScopeId id, Name name, string description)
            : base(id, name, description)
        {
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
