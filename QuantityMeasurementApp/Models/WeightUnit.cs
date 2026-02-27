using System;

using QuantityMeasurementApp.Models;
namespace QuantityMeasurementApp.Models
{
    public enum WeightUnit
    {
        Kilogram = 0,
        Gram = 1,
        Pound = 2
    }

    public static class WeightUnitExtensions
    {
        public static double GetConversionFactor(this WeightUnit unit)
        {
            switch (unit)
            {
                case WeightUnit.Kilogram:
                    return 1.0;
                case WeightUnit.Gram:
                    return 0.001;
                case WeightUnit.Pound:
                    return 0.453592;
                default:
                    throw new ArgumentException("Unsupported unit");
            }
        }

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

        public static string GetUnitName(this WeightUnit unit)
        {
            return unit.ToString().ToUpper();
        }
    }
}
