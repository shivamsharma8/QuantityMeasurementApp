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

    public static class LengthUnitExtensions
    {
        public static double ToBaseUnit(this LengthUnit unit, double value)
        {
            // Base unit: Feet
            return unit switch
            {
                LengthUnit.Feet => value,
                LengthUnit.Inch => value / 12.0,
                LengthUnit.Yard => value * 3.0,
                LengthUnit.Centimeter => (value * 0.393701) / 12.0,
                _ => throw new ArgumentException("Unsupported unit")
            };
        }
    }
}
