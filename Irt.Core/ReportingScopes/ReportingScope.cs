using Irt.Core.SeedWork;
using Irt.Core.SharedKernel;
using Irt.Core.ValueObjects;
using MongoDB.Bson;

namespace Irt.Core.ReportingScopes
{
    public class ReportingScope : Entity<ReportingScopeId>
    {
        public string Description { get; private set; }
        public Name ReportingScopeName { get; private set; }

        private ReportingScope(ReportingScopeId id, Name name, string description)
        {
            Id = id;
            ReportingScopeName = name;
            Description = description;
        }

        public static ReportingScope CreateReportingScope(Name name, string description, INameValidationChecker<ReportingScope> nameValidationChecker)
        {
            CheckRule(new NameUniquenessChecker<ReportingScope>(nameValidationChecker, name, null));
            return new ReportingScope(new ReportingScopeId(ObjectId.GenerateNewId().ToString()), name, description);
        }
    }
}