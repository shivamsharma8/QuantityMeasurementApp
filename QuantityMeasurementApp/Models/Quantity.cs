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

        private enum ArithmeticOperation
        {
            Add,
            Subtract,
            Divide
        }

        private static double PerformArithmetic(Quantity<U> a, Quantity<U> b, ArithmeticOperation op)
        {
            if (a == null || b == null)
                throw new ArgumentException("Operands must not be null.");
            if (!a.Unit.GetType().Equals(b.Unit.GetType()))
                throw new ArgumentException("Cannot operate on quantities of different categories.");
            if (double.IsNaN(a.Value) || double.IsInfinity(a.Value) || double.IsNaN(b.Value) || double.IsInfinity(b.Value))
                throw new ArgumentException("Values must be finite numbers.");
            var measurableA = GetMeasurable(a.Unit);
            var measurableB = GetMeasurable(b.Unit);
            double aBase = measurableA.ConvertToBaseUnit(a.Value);
            double bBase = measurableB.ConvertToBaseUnit(b.Value);
            switch (op)
            {
                case ArithmeticOperation.Add:
                    return aBase + bBase;
                case ArithmeticOperation.Subtract:
                    return aBase - bBase;
                case ArithmeticOperation.Divide:
                    if (Math.Abs(bBase) < 0.000001)
                        throw new DivideByZeroException("Cannot divide by zero quantity.");
                    return aBase / bBase;
                default:
                    throw new InvalidOperationException("Unsupported arithmetic operation.");
            }
        }

        private static void ValidateArithmeticSupport(U unit, string operation)
        {
            var measurable = GetMeasurable(unit);
            measurable.ValidateOperationSupport(operation);
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
            ValidateArithmeticSupport(a.Unit, "Add");
            ValidateArithmeticSupport(b.Unit, "Add");
            double sumBase = PerformArithmetic(a, b, ArithmeticOperation.Add);
            var measurableA = GetMeasurable(a.Unit);
            double sumInAUnit = measurableA.ConvertFromBaseUnit(sumBase);
            return new Quantity<U>(Math.Round(sumInAUnit, 6), a.Unit);
        }

        public static Quantity<U> Add(Quantity<U> a, Quantity<U> b, U targetUnit)
        {
            ValidateArithmeticSupport(a.Unit, "Add");
            ValidateArithmeticSupport(b.Unit, "Add");
            ValidateArithmeticSupport(targetUnit, "Add");
            if (a == null || b == null)
                throw new ArgumentException("Operands must not be null.");
            if (!a.Unit.GetType().Equals(b.Unit.GetType()) || !a.Unit.GetType().Equals(targetUnit.GetType()))
                throw new ArgumentException("Cannot add quantities of different categories.");
            double sumBase = PerformArithmetic(a, b, ArithmeticOperation.Add);
            var measurableTarget = GetMeasurable(targetUnit);
            double sumInTargetUnit = measurableTarget.ConvertFromBaseUnit(sumBase);
            return new Quantity<U>(Math.Round(sumInTargetUnit, 6), targetUnit);
        }

        public static Quantity<U> Subtract(Quantity<U> a, Quantity<U> b)
        {
            ValidateArithmeticSupport(a.Unit, "Subtract");
            ValidateArithmeticSupport(b.Unit, "Subtract");
            double diffBase = PerformArithmetic(a, b, ArithmeticOperation.Subtract);
            var measurableA = GetMeasurable(a.Unit);
            double diffInAUnit = measurableA.ConvertFromBaseUnit(diffBase);
            return new Quantity<U>(Math.Round(diffInAUnit, 6), a.Unit);
        }

        public static Quantity<U> Subtract(Quantity<U> a, Quantity<U> b, U targetUnit)
        {
            ValidateArithmeticSupport(a.Unit, "Subtract");
            ValidateArithmeticSupport(b.Unit, "Subtract");
            ValidateArithmeticSupport(targetUnit, "Subtract");
            if (a == null || b == null)
                throw new ArgumentException("Operands must not be null.");
            if (!a.Unit.GetType().Equals(b.Unit.GetType()) || !a.Unit.GetType().Equals(targetUnit.GetType()))
                throw new ArgumentException("Cannot subtract quantities of different categories.");
            double diffBase = PerformArithmetic(a, b, ArithmeticOperation.Subtract);
            var measurableTarget = GetMeasurable(targetUnit);
            double diffInTargetUnit = measurableTarget.ConvertFromBaseUnit(diffBase);
            return new Quantity<U>(Math.Round(diffInTargetUnit, 6), targetUnit);
        }

        public Quantity<U> Add(Quantity<U> other)
        {
            return Add(this, other);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            return Add(this, other, targetUnit);
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
            ValidateArithmeticSupport(Unit, "Divide");
            ValidateArithmeticSupport(other.Unit, "Divide");
            return Math.Round(PerformArithmetic(this, other, ArithmeticOperation.Divide), 6);
        }

        public override string ToString()
        {
            var measurableUnit = (IMeasurable)(object)Unit;
            return $"Quantity({Value}, {measurableUnit.GetUnitName()})";
        }
    }
}
