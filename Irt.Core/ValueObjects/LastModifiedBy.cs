using Irt.Core.SeedWork;

namespace Irt.Core.ValueObjects
{
    public class LastModifiedBy : ValueObject
    {
        public string UserId { get; }
        public string UserName { get; }
        public string Application { get; }
        public DateTime Timestamp { get; }
        public string IpAddress { get; }

        private LastModifiedBy(string userId, string userName, string application, DateTime modifiedAt, string ipAddress)
        {
            ValidateNotNullOrWhitespace(userId, nameof(userId));
            ValidateNotNullOrWhitespace(userName, nameof(userName));
            ValidateNotNullOrWhitespace(application, nameof(application));

            UserId = userId;
            UserName = userName;
            Application = application;
            Timestamp = modifiedAt;
            IpAddress = ipAddress;
        }

        // Required by some ORMs for deserialization
        private LastModifiedBy() { }

        public static LastModifiedBy Create(string userId, string userName, string application, 
            DateTime? modifiedAt = null, string ipAddress = null)
        {
            return new LastModifiedBy(
                userId, 
                userName, 
                application, 
                modifiedAt ?? DateTime.UtcNow,
                ipAddress ?? "127.0.0.1"
            );
        }

        // Simplified constructor for cases with minimal information
        public static LastModifiedBy Create(string userId, string userName, string application, string ipAddress)
        {
            return Create(userId, userName, application, DateTime.UtcNow, ipAddress);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return UserName;
            yield return Application;
            yield return Timestamp;
            yield return IpAddress ?? string.Empty;
        }

        public override string ToString() =>
            $"{UserName} ({UserId}) via {Application} at {Timestamp:O}" +
            (string.IsNullOrWhiteSpace(IpAddress) ? string.Empty : $" from {IpAddress}");

        private static void ValidateNotNullOrWhitespace(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", paramName);
            }
        }
    }
}