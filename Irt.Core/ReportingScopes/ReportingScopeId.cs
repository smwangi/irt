using Irt.Core.SeedWork;

namespace Irt.Core.ReportingScopes
{
    public class ReportingScopeId(string value) : TypedIdValueBase<ReportingScopeId>(value)
    {
        public string Value { get; } = value;
    }
}