using System;

namespace SimpleSamples
{
    public class Rating : IEquatable<Rating>, IComparable<Rating>
    {
        public Rating(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public int CompareTo(Rating other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }
            if (ReferenceEquals(null, other))
            {
                return 1;
            }
            return Value.CompareTo(other.Value);
        }

        public bool Equals(Rating other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((Rating) obj);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(Rating left, Rating right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Rating left, Rating right)
        {
            return !Equals(left, right);
        }
    }
}