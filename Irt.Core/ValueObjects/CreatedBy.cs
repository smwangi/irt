using Irt.Core.SeedWork;

namespace Irt.Core.ValueObjects;
    public class CreatedBy : ValueObject
    {
        public string UserId { get; }
        public string UserName { get; }
        public string Application { get; }
        public DateTime CreatedAt { get; }
        public string IpAddress { get; }

        private CreatedBy(string userId, string userName, string application, DateTime createdAt, string ipAddress)
        {
            ValidateNotNullOrWhitespace(userId, nameof(userId));
            ValidateNotNullOrWhitespace(userName, nameof(userName));
            ValidateNotNullOrWhitespace(application, nameof(application));
            ValidateNotNullOrWhitespace(ipAddress, nameof(ipAddress));

            UserId = userId;
            UserName = userName;
            Application = application;
            CreatedAt = createdAt;
            IpAddress = ipAddress;
        }

        // Required by some ORMs for deserialization
        private CreatedBy()
        {
        }

        public static CreatedBy Create(string userId, string userName, string application,
            DateTime? createdAt = null, string ipAddress = null)
        {
            return new CreatedBy(
                userId,
                userName,
                application,
                createdAt ?? DateTime.UtcNow,
                ipAddress ?? "127.0.0.1"
            );
        }

        // Static factory method with validation for all required fields
        public static CreatedBy Create(string userId, string userName, string application, string ipAddress)
        {
            return Create(userId, userName, application, DateTime.UtcNow, ipAddress);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return UserName;
            yield return Application;
            yield return CreatedAt;
            yield return IpAddress;
        }

        public override string ToString() =>
            $"{UserName} ({UserId}) via {Application} at {CreatedAt:O}" +
            (string.IsNullOrWhiteSpace(IpAddress) ? string.Empty : $" from {IpAddress}");

        private static void ValidateNotNullOrWhitespace(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", paramName);
            }
        }
    }
