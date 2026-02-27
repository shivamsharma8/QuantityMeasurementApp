using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents a length measurement with unit and provides equality and conversion operations.
    /// </summary>
    public class QuantityLength : IEquatable<QuantityLength>
    {
        public double Value { get; }
        public LengthUnit Unit { get; }

        public QuantityLength(double value, LengthUnit unit)
        {
            if (!Enum.IsDefined(typeof(LengthUnit), unit))
                throw new ArgumentException("Invalid unit type");
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be a finite number");
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
            double thisBase = Unit.ConvertToBaseUnit(Value);
            double otherBase = other.Unit.ConvertToBaseUnit(other.Value);
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

        /// <summary>
        /// Converts a value from source unit to target unit.
        /// </summary>
        /// <param name="value">The numeric value to convert.</param>
        /// <param name="source">The source unit.</param>
        /// <param name="target">The target unit.</param>
        /// <returns>The converted value in the target unit.</returns>
        /// <exception cref="ArgumentException">Thrown for invalid units or values.</exception>
        public static double Convert(double value, LengthUnit source, LengthUnit target)
        {
            if (!Enum.IsDefined(typeof(LengthUnit), source) || !Enum.IsDefined(typeof(LengthUnit), target))
                throw new ArgumentException("Invalid unit type");
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be a finite number");
            // Convert to base unit (feet)
            double baseValue = source.ConvertToBaseUnit(value);
            // Convert from base unit to target
            double result = target.ConvertFromBaseUnit(baseValue);
            return Math.Round(result, 6); // Precision handling
        }

        /// <summary>
        /// Converts this instance to the target unit.
        /// </summary>
        /// <param name="target">The target unit.</param>
        /// <returns>A new QuantityLength instance in the target unit.</returns>
        public QuantityLength ConvertTo(LengthUnit target)
        {
            double converted = Convert(Value, Unit, target);
            return new QuantityLength(converted, target);
        }

        public static QuantityLength Add(QuantityLength a, QuantityLength b)
        {
            if (a == null || b == null)
                throw new ArgumentException("Operands must not be null.");
            if (!Enum.IsDefined(typeof(LengthUnit), a.Unit) || !Enum.IsDefined(typeof(LengthUnit), b.Unit))
                throw new ArgumentException("Invalid unit type.");
            if (!double.IsFinite(a.Value) || !double.IsFinite(b.Value))
                throw new ArgumentException("Values must be finite numbers.");
            // Convert both to base unit (feet)
            double aBase = a.Unit.ConvertToBaseUnit(a.Value);
            double bBase = b.Unit.ConvertToBaseUnit(b.Value);
            double sumBase = aBase + bBase;
            // Convert sum to unit of first operand
            double sumInAUnit = a.Unit.ConvertFromBaseUnit(sumBase);
            return new QuantityLength(sumInAUnit, a.Unit);
        }

        /// <summary>
        /// Adds two QuantityLength objects and returns the sum in the specified target unit.
        /// </summary>
        /// <param name="a">First QuantityLength operand.</param>
        /// <param name="b">Second QuantityLength operand.</param>
        /// <param name="targetUnit">The unit for the result.</param>
        /// <returns>New QuantityLength in the target unit.</returns>
        /// <exception cref="ArgumentException">Thrown for invalid units or values.</exception>
        public static QuantityLength Add(QuantityLength a, QuantityLength b, LengthUnit targetUnit)
        {
            if (a == null || b == null)
                throw new ArgumentException("Operands must not be null.");
            if (!Enum.IsDefined(typeof(LengthUnit), a.Unit) || !Enum.IsDefined(typeof(LengthUnit), b.Unit) || !Enum.IsDefined(typeof(LengthUnit), targetUnit))
                throw new ArgumentException("Invalid unit type.");
            if (!double.IsFinite(a.Value) || !double.IsFinite(b.Value))
                throw new ArgumentException("Values must be finite numbers.");
            // Convert both to base unit (feet)
            double aBase = a.Unit.ConvertToBaseUnit(a.Value);
            double bBase = b.Unit.ConvertToBaseUnit(b.Value);
            double sumBase = aBase + bBase;
            // Convert sum to target unit
            double sumInTargetUnit = targetUnit.ConvertFromBaseUnit(sumBase);
            return new QuantityLength(sumInTargetUnit, targetUnit);
        }

        /// <summary>
        /// Adds another QuantityLength to this instance and returns the sum in the specified target unit.
        /// </summary>
        /// <param name="other">Other QuantityLength operand.</param>
        /// <param name="targetUnit">The unit for the result.</param>
        /// <returns>New QuantityLength in the target unit.</returns>
        public QuantityLength Add(QuantityLength other, LengthUnit targetUnit)
        {
            return Add(this, other, targetUnit);
        }

        public QuantityLength Add(QuantityLength other)
        {
            return Add(this, other);
        }
    }
}
