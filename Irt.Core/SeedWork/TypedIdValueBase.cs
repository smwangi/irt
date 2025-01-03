using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Irt.Core.SeedWork
{
    public class TypedIdValueBase : IEquatable<TypedIdValueBase>
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; }
        protected TypedIdValueBase(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id cannot be empty", nameof(id));
            }
            
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            return obj is TypedIdValueBase other && Equals(other);
        }

        public bool Equals(TypedIdValueBase? other)
        {
            if (other is null) return false;
            return Id == other.Id;
        }

        public static implicit operator string(TypedIdValueBase typedId)
        {
            return typedId.Id;
        }

        public static implicit operator TypedIdValueBase(string value)
        {
            return new TypedIdValueBase(value);
        }     

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(TypedIdValueBase a, TypedIdValueBase b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null)) return false;
            return a.Equals(b);
        }

        public static bool operator !=(TypedIdValueBase a, TypedIdValueBase b)
        {
            return !(a == b);
        }

        public static TypedIdValueBase Of(string id)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id cannot be empty", nameof(id));
            }

            return new TypedIdValueBase(id);
        }

        public static TypedIdValueBase FromString(string id)
        {
            return new TypedIdValueBase(id);
        }

        public override string ToString()
        {
            return Id;
        }
    }
}