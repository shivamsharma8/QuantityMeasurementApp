using System;

namespace QuantityMeasurementApp.Models
{
    public enum WeightUnit
    {
        Kilogram,
        Gram,
        Pound
    }

    public static class WeightUnitConversion
    {
        // Converts a value in this unit to the base unit (kilogram)
        public static double ConvertToBaseUnit(this WeightUnit unit, double value)
        {
            switch (unit)
            {
                case WeightUnit.Kilogram:
                    return value;
                case WeightUnit.Gram:
                    return value / 1000.0;
                case WeightUnit.Pound:
                    return value * 0.453592;
                default:
                    throw new ArgumentException("Unsupported unit");
            }
        }

        // Converts a value in the base unit (kilogram) to this unit
        public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
        {
            switch (unit)
            {
                case WeightUnit.Kilogram:
                    return baseValue;
                case WeightUnit.Gram:
                    return baseValue * 1000.0;
                case WeightUnit.Pound:
                    return baseValue / 0.453592;
                default:
                    throw new ArgumentException("Unsupported unit");
            }
        }
    }
}
