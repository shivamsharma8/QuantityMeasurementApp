using System;
namespace QuantityMeasurementApp.Models
{
    public enum LengthUnit
    {
        Feet,
        Inch,
        Yard,
        Centimeter
    }

    public static class LengthUnitConversion
    {
        // Converts a value in this unit to the base unit (feet)
        public static double ConvertToBaseUnit(this LengthUnit unit, double value)
        {
            switch (unit)
            {
                case LengthUnit.Feet:
                    return value;
                case LengthUnit.Inch:
                    return value / 12.0;
                case LengthUnit.Yard:
                    return value * 3.0;
                case LengthUnit.Centimeter:
                    return value / 30.48;
                default:
                    throw new ArgumentException("Unsupported unit");
            }
        }

        // Converts a value in the base unit (feet) to this unit
        public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
        {
            switch (unit)
            {
                case LengthUnit.Feet:
                    return baseValue;
                case LengthUnit.Inch:
                    return baseValue * 12.0;
                case LengthUnit.Yard:
                    return baseValue / 3.0;
                case LengthUnit.Centimeter:
                    return baseValue * 30.48;
                default:
                    throw new ArgumentException("Unsupported unit");
            }
        }
    }
}
