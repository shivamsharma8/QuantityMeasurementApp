using System;

namespace QuantityMeasurementApp.Models
{
    public sealed class Feet : IEquatable<Feet>
    {
        public double Value { get; }

        public Feet(double value)
        {
            Value = value;
        }

        public bool Equals(Feet other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Value.CompareTo(other.Value) == 0;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Feet);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Feet left, Feet right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);

            return left.Equals(right);
        }

        public static bool operator !=(Feet left, Feet right)
        {
            return !(left == right);
        }
    }
}