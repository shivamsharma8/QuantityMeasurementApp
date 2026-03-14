using QuantityMeasurementModelLayer;
using QuantityMeasurementBusinessLayer.Interfaces;


namespace QuantityMeasurementBusinessLayer
{

    public static class TemperatureUnitExtensions
    {
        public static double GetConversionFactor(this TemperatureUnit unit)
        {
            // Not used for temperature, but required for interface compatibility
            return 1.0;
        }

        public static double ConvertToBaseUnit(this TemperatureUnit unit, double value)
        {
            // Base unit is Celsius
            switch (unit)
            {
                case TemperatureUnit.CELSIUS:
                    return value;
                case TemperatureUnit.FAHRENHEIT:
                    return (value - 32) * 5.0 / 9.0;
                default:
                    throw new ArgumentException("Invalid TemperatureUnit");
            }
        }

        public static double ConvertFromBaseUnit(this TemperatureUnit unit, double baseValue)
        {
            switch (unit)
            {
                case TemperatureUnit.CELSIUS:
                    return baseValue;
                case TemperatureUnit.FAHRENHEIT:
                    return baseValue * 9.0 / 5.0 + 32;
                default:
                    throw new ArgumentException("Invalid TemperatureUnit");
            }
        }

        public static string GetUnitName(this TemperatureUnit unit)
        {
            switch (unit)
            {
                case TemperatureUnit.CELSIUS:
                    return "Celsius";
                case TemperatureUnit.FAHRENHEIT:
                    return "Fahrenheit";
                default:
                    throw new ArgumentException("Invalid TemperatureUnit");
            }
        }

        public static IMeasurable AsMeasurable(this TemperatureUnit unit)
        {
            return new TemperatureUnitMeasurable(unit);
        }

        private class TemperatureUnitMeasurable : IMeasurable
        {
            private readonly TemperatureUnit unit;
            public TemperatureUnitMeasurable(TemperatureUnit unit) { this.unit = unit; }
            public double GetConversionFactor() => unit.GetConversionFactor();
            public double ConvertToBaseUnit(double value) => unit.ConvertToBaseUnit(value);
            public double ConvertFromBaseUnit(double baseValue) => unit.ConvertFromBaseUnit(baseValue);
            public string GetUnitName() => unit.GetUnitName();
            public void ValidateOperationSupport(string operation)
            {
                // Only equality and conversion supported
                if (operation == "Add" || operation == "Subtract" || operation == "Divide")
                    throw new NotSupportedException($"Operation '{operation}' is not supported for temperature measurements.");
            }
        }
    }
}