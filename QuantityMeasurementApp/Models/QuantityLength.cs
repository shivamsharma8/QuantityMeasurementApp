using System;

namespace QuantityMeasurementApp.Models
{
    public class QuantityLength : IEquatable<QuantityLength>
    {
        public double Value { get; }
        public LengthUnit Unit { get; }

        public QuantityLength(double value, LengthUnit unit)
        {
            if (!Enum.IsDefined(typeof(LengthUnit), unit))
                throw new ArgumentException("Invalid unit type");
            Value = value;
            Unit = unit;
        }

        public bool Equals(QuantityLength other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            // Convert both to base unit (feet) and compare
            double thisBase = Unit.ToBaseUnit(Value);
            double otherBase = other.Unit.ToBaseUnit(other.Value);
            return Math.Abs(thisBase - otherBase) < 0.0001;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as QuantityLength);
        }

        public override int GetHashCode()
        {
            return Unit.GetHashCode() ^ Value.GetHashCode();
        }
    }
}
