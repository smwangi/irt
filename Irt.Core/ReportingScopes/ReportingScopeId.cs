using Irt.Core.SeedWork;

namespace Irt.Core.ReportingScopes
{
    public class ReportingScopeId: TypedIdValueBase<ReportingScopeId>
    {
        public string Value { get; }
        private ReportingScopeId(string value) : base(value)
        {
            Value = value;
        }
        private ReportingScopeId() : base(string.Empty)
        {
        }
        
        public static ReportingScopeId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("ReportingScopeId cannot be empty", nameof(value));
            }
            
            return new ReportingScopeId(value);
        }
    }
}