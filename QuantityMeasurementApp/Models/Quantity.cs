using System;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Generic quantity class for any measurable unit category.
    /// </summary>
    /// <typeparam name="U">Unit type implementing IMeasurable</typeparam>
    public class Quantity<U> where U : struct, Enum
    {
        public double Value { get; }
        public U Unit { get; }

        public Quantity(double value, U unit)
        {
            if (!Enum.IsDefined(typeof(U), unit))
                throw new ArgumentException("Invalid unit type");
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be a finite number");
            Value = value;
            Unit = unit;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj == null || obj.GetType() != GetType()) return false;
            var other = (Quantity<U>)obj;
            if (!Unit.GetType().Equals(other.Unit.GetType())) return false;
            double thisBase = GetMeasurable(Unit).ConvertToBaseUnit(Value);
            double otherBase = GetMeasurable(other.Unit).ConvertToBaseUnit(other.Value);
            return Math.Abs(thisBase - otherBase) < 0.0001;
        }

        private static IMeasurable GetMeasurable(U unit)
        {
            if (typeof(U) == typeof(LengthUnit))
                return ((LengthUnit)(object)unit).AsMeasurable();
            if (typeof(U) == typeof(WeightUnit))
                return ((WeightUnit)(object)unit).AsMeasurable();
            if (typeof(U) == typeof(VolumeUnit))
                return ((VolumeUnit)(object)unit).AsMeasurable();
            throw new ArgumentException("Unsupported unit type");
        }
        

        public override int GetHashCode()
        {
            return Unit.GetHashCode() ^ Value.GetHashCode();
        }

        public Quantity<U> ConvertTo(U targetUnit)
        {
            var measurableUnit = (IMeasurable)(object)Unit;
            var measurableTarget = (IMeasurable)(object)targetUnit;
            double baseValue = measurableUnit.ConvertToBaseUnit(Value);
            double converted = measurableTarget.ConvertFromBaseUnit(baseValue);
            return new Quantity<U>(Math.Round(converted, 6), targetUnit);
        }

        public static Quantity<U> Add(Quantity<U> a, Quantity<U> b)
        {
            if (a == null || b == null)
                throw new ArgumentException("Operands must not be null.");
            if (!a.Unit.GetType().Equals(b.Unit.GetType()))
                throw new ArgumentException("Cannot add quantities of different categories.");
            var measurableA = (IMeasurable)(object)a.Unit;
            var measurableB = (IMeasurable)(object)b.Unit;
            double aBase = measurableA.ConvertToBaseUnit(a.Value);
            double bBase = measurableB.ConvertToBaseUnit(b.Value);
            double sumBase = aBase + bBase;
            double sumInAUnit = measurableA.ConvertFromBaseUnit(sumBase);
            return new Quantity<U>(Math.Round(sumInAUnit, 6), a.Unit);
        }

        public static Quantity<U> Add(Quantity<U> a, Quantity<U> b, U targetUnit)
        {
            if (a == null || b == null)
                throw new ArgumentException("Operands must not be null.");
            if (!a.Unit.GetType().Equals(b.Unit.GetType()) || !a.Unit.GetType().Equals(targetUnit.GetType()))
                throw new ArgumentException("Cannot add quantities of different categories.");
            var measurableA = (IMeasurable)(object)a.Unit;
            var measurableB = (IMeasurable)(object)b.Unit;
            var measurableTarget = (IMeasurable)(object)targetUnit;
            double aBase = measurableA.ConvertToBaseUnit(a.Value);
            double bBase = measurableB.ConvertToBaseUnit(b.Value);
            double sumBase = aBase + bBase;
            double sumInTargetUnit = measurableTarget.ConvertFromBaseUnit(sumBase);
            return new Quantity<U>(Math.Round(sumInTargetUnit, 6), targetUnit);
        }

        public Quantity<U> Add(Quantity<U> other)
        {
            return Add(this, other);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            return Add(this, other, targetUnit);
        }

        public static Quantity<U> Subtract(Quantity<U> a, Quantity<U> b)
        {
            if (a == null || b == null)
                throw new ArgumentException("Operands must not be null.");
            if (!a.Unit.GetType().Equals(b.Unit.GetType()))
                throw new ArgumentException("Cannot subtract quantities of different categories.");
            var measurableA = GetMeasurable(a.Unit);
            var measurableB = GetMeasurable(b.Unit);
            double aBase = measurableA.ConvertToBaseUnit(a.Value);
            double bBase = measurableB.ConvertToBaseUnit(b.Value);
            double diffBase = aBase - bBase;
            double diffInAUnit = measurableA.ConvertFromBaseUnit(diffBase);
            return new Quantity<U>(Math.Round(diffInAUnit, 6), a.Unit);
        }

        public static Quantity<U> Subtract(Quantity<U> a, Quantity<U> b, U targetUnit)
        {
            if (a == null || b == null)
                throw new ArgumentException("Operands must not be null.");
            if (!a.Unit.GetType().Equals(b.Unit.GetType()) || !a.Unit.GetType().Equals(targetUnit.GetType()))
                throw new ArgumentException("Cannot subtract quantities of different categories.");
            var measurableA = GetMeasurable(a.Unit);
            var measurableB = GetMeasurable(b.Unit);
            var measurableTarget = GetMeasurable(targetUnit);
            double aBase = measurableA.ConvertToBaseUnit(a.Value);
            double bBase = measurableB.ConvertToBaseUnit(b.Value);
            double diffBase = aBase - bBase;
            double diffInTargetUnit = measurableTarget.ConvertFromBaseUnit(diffBase);
            return new Quantity<U>(Math.Round(diffInTargetUnit, 6), targetUnit);
        }

        public Quantity<U> Subtract(Quantity<U> other)
        {
            return Subtract(this, other);
        }

        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            return Subtract(this, other, targetUnit);
        }

        public double Divide(Quantity<U> other)
        {
            if (other == null)
                throw new ArgumentException("Operand must not be null.");
            if (!Unit.GetType().Equals(other.Unit.GetType()))
                throw new ArgumentException("Cannot divide quantities of different categories.");
            var measurableA = GetMeasurable(Unit);
            var measurableB = GetMeasurable(other.Unit);
            double aBase = measurableA.ConvertToBaseUnit(Value);
            double bBase = measurableB.ConvertToBaseUnit(other.Value);
            if (Math.Abs(bBase) < 0.000001)
                throw new DivideByZeroException("Cannot divide by zero quantity.");
            return Math.Round(aBase / bBase, 6);
        }

        public override string ToString()
        {
            var measurableUnit = (IMeasurable)(object)Unit;
            return $"Quantity({Value}, {measurableUnit.GetUnitName()})";
        }
    }
}
