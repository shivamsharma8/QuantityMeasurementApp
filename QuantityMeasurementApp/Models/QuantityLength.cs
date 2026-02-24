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
            double baseValue = source.ToBaseUnit(value);
            // Convert from base unit to target
            double result = target switch
            {
                LengthUnit.Feet => baseValue,
                LengthUnit.Inch => baseValue * 12.0,
                LengthUnit.Yard => baseValue / 3.0,
                LengthUnit.Centimeter => (baseValue * 12.0) / 0.393701,
                _ => throw new ArgumentException("Unsupported unit")
            };
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
    }
}
