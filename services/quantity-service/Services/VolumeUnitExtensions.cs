using QuantityMeasurementModelLayer;
using QuantityMeasurementBusinessLayer.Interfaces;


namespace QuantityMeasurementBusinessLayer
{

    public static class VolumeUnitExtensions
    {
        public static double GetConversionFactor(this VolumeUnit unit)
        {
            switch (unit)
            {
                case VolumeUnit.LITRE:
                    return 1.0;
                case VolumeUnit.MILLILITRE:
                    return 0.001;
                case VolumeUnit.GALLON:
                    return 3.78541;
                default:
                    throw new ArgumentException("Invalid VolumeUnit");
            }
        }

        public static double ConvertToBaseUnit(this VolumeUnit unit, double value)
        {
            return value * unit.GetConversionFactor();
        }

        public static double ConvertFromBaseUnit(this VolumeUnit unit, double baseValue)
        {
            switch (unit)
            {
                case VolumeUnit.LITRE:
                    return baseValue;
                case VolumeUnit.MILLILITRE:
                    return baseValue / 0.001;
                case VolumeUnit.GALLON:
                    return baseValue / 3.78541;
                default:
                    throw new ArgumentException("Invalid VolumeUnit");
            }
        }

        public static string GetUnitName(this VolumeUnit unit)
        {
            switch (unit)
            {
                case VolumeUnit.LITRE:
                    return "Litre";
                case VolumeUnit.MILLILITRE:
                    return "Millilitre";
                case VolumeUnit.GALLON:
                    return "Gallon";
                default:
                    throw new ArgumentException("Invalid VolumeUnit");
            }
        }

        public static IMeasurable AsMeasurable(this VolumeUnit unit)
        {
            return new VolumeUnitMeasurable(unit);
        }

        private class VolumeUnitMeasurable : IMeasurable
        {
            private readonly VolumeUnit unit;
            public VolumeUnitMeasurable(VolumeUnit unit) { this.unit = unit; }
            public double GetConversionFactor() => unit.GetConversionFactor();
            public double ConvertToBaseUnit(double value) => unit.ConvertToBaseUnit(value);
            public double ConvertFromBaseUnit(double baseValue) => unit.ConvertFromBaseUnit(baseValue);
            public string GetUnitName() => unit.GetUnitName();
        }
    }
}