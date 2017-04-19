using System;

namespace SimpleSamples
{
    public class SimpleUser : IEquatable<SimpleUser>
    {
        public Gender Gender { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Joined { get; set; }
        public decimal Score { get; set; }

        public bool Equals(SimpleUser other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Gender == other.Gender && string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase) && Age == other.Age && Joined.Equals(other.Joined) &&
                   Score == other.Score;
        }

        public override string ToString()
        {
            return $"{Name} - {Gender}; Age: {Age}; Joined: {Joined}; Score: {Score}";
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
            return Equals((SimpleUser) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Gender;
                hashCode = (hashCode * 397) ^ (Name != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(Name) : 0);
                hashCode = (hashCode * 397) ^ Age;
                hashCode = (hashCode * 397) ^ Joined.GetHashCode();
                hashCode = (hashCode * 397) ^ Score.GetHashCode();
                return hashCode;
            }
        }
    }
}