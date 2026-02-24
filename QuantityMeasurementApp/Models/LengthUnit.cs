namespace QuantityMeasurementApp.Models
{
    public enum LengthUnit
    {
        Feet,
        Inch
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
                _ => throw new ArgumentException("Unsupported unit")
            };
        }
    }
}
