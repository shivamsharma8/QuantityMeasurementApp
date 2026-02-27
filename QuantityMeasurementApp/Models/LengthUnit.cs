using System;
using QuantityMeasurementApp.Models;
namespace QuantityMeasurementApp.Models
{
    public enum LengthUnit
    {
        Feet = 0,
        Inch = 1,
        Yard = 2,
        Centimeter = 3
    }

    public static class LengthUnitExtensions
    {
        public static double GetConversionFactor(this LengthUnit unit)
        {
            switch (unit)
            {
                case LengthUnit.Feet:
                    return 1.0;
                case LengthUnit.Inch:
                    return 1.0 / 12.0;
                case LengthUnit.Yard:
                    return 3.0;
                case LengthUnit.Centimeter:
                    return 1.0 / 30.48;
                default:
                    throw new ArgumentException("Unsupported unit");
            }
        }

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

        public static string GetUnitName(this LengthUnit unit)
        {
            return unit.ToString().ToUpper();
        }
    }
}
