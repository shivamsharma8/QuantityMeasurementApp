using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents a weight measurement with unit and provides equality, conversion, and addition operations.
    /// </summary>
    public class QuantityWeight : IEquatable<QuantityWeight>
    {
        public double Value { get; }
        public WeightUnit Unit { get; }

        public QuantityWeight(double value, WeightUnit unit)
        {
            if (!Enum.IsDefined(typeof(WeightUnit), unit))
                throw new ArgumentException("Invalid unit type");
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be a finite number");
            Value = value;
            Unit = unit;
        }

        public bool Equals(QuantityWeight other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            // Convert both to base unit (kilogram) and compare
            double thisBase = Unit.ConvertToBaseUnit(Value);
            double otherBase = other.Unit.ConvertToBaseUnit(other.Value);
            return Math.Abs(thisBase - otherBase) < 0.0001;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as QuantityWeight);
        }

        public override int GetHashCode()
        {
            return Unit.GetHashCode() ^ Value.GetHashCode();
        }

        public static double Convert(double value, WeightUnit source, WeightUnit target)
        {
            if (!Enum.IsDefined(typeof(WeightUnit), source) || !Enum.IsDefined(typeof(WeightUnit), target))
                throw new ArgumentException("Invalid unit type");
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be a finite number");
            double baseValue = source.ConvertToBaseUnit(value);
            double result = target.ConvertFromBaseUnit(baseValue);
            return Math.Round(result, 6);
        }

        public QuantityWeight ConvertTo(WeightUnit target)
        {
            double converted = Convert(Value, Unit, target);
            return new QuantityWeight(converted, target);
        }

        public static QuantityWeight Add(QuantityWeight a, QuantityWeight b)
        {
            if (a == null || b == null)
                throw new ArgumentException("Operands must not be null.");
            if (!Enum.IsDefined(typeof(WeightUnit), a.Unit) || !Enum.IsDefined(typeof(WeightUnit), b.Unit))
                throw new ArgumentException("Invalid unit type.");
            if (!double.IsFinite(a.Value) || !double.IsFinite(b.Value))
                throw new ArgumentException("Values must be finite numbers.");
            double aBase = a.Unit.ConvertToBaseUnit(a.Value);
            double bBase = b.Unit.ConvertToBaseUnit(b.Value);
            double sumBase = aBase + bBase;
            double sumInAUnit = a.Unit.ConvertFromBaseUnit(sumBase);
            return new QuantityWeight(sumInAUnit, a.Unit);
        }

        public static QuantityWeight Add(QuantityWeight a, QuantityWeight b, WeightUnit targetUnit)
        {
            if (a == null || b == null)
                throw new ArgumentException("Operands must not be null.");
            if (!Enum.IsDefined(typeof(WeightUnit), a.Unit) || !Enum.IsDefined(typeof(WeightUnit), b.Unit) || !Enum.IsDefined(typeof(WeightUnit), targetUnit))
                throw new ArgumentException("Invalid unit type.");
            if (!double.IsFinite(a.Value) || !double.IsFinite(b.Value))
                throw new ArgumentException("Values must be finite numbers.");
            double aBase = a.Unit.ConvertToBaseUnit(a.Value);
            double bBase = b.Unit.ConvertToBaseUnit(b.Value);
            double sumBase = aBase + bBase;
            double sumInTargetUnit = targetUnit.ConvertFromBaseUnit(sumBase);
            return new QuantityWeight(sumInTargetUnit, targetUnit);
        }

        public QuantityWeight Add(QuantityWeight other)
        {
            return Add(this, other);
        }

        public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
        {
            return Add(this, other, targetUnit);
        }

        public override string ToString()
        {
            return $"Quantity({Value}, {Unit})";
        }
    }
}
