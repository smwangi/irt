

namespace Irt.Core.SeedWork
{
    public abstract class TypedIdValueBase<T> : IEquatable<TypedIdValueBase<T>>
    {
        public string Value { get; }
        protected TypedIdValueBase(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Id cannot be empty", nameof(value));
            }
            
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            return obj is TypedIdValueBase<T> other && Equals(other);
        }

        public bool Equals(TypedIdValueBase<T>? other)
        {
            if (other is null) return false;
            return Value == other.Value;
        }

        public static implicit operator string(TypedIdValueBase<T> typedId)
        {
            return typedId.Value;
        }

        /*public static implicit operator TypedIdValueBase(string value)
        {
            return new TypedIdValueBase(value);
        }*/
        
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(TypedIdValueBase<T> a, TypedIdValueBase<T> b)
        {
            if (ReferenceEquals(a, b)) return true;
            return !ReferenceEquals(a, null) && a.Equals(b);
        }

        public static bool operator !=(TypedIdValueBase<T> a, TypedIdValueBase<T> b)
        {
            return !(a == b);
        }

        /*public static TypedIdValueBase Of(string id)
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
        }*/

        public override string ToString() => Value;
    }
}