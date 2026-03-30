namespace QuantityMeasurementBusinessLayer.Interfaces
{
    public interface IMeasurable
    {
        double GetConversionFactor();
        double ConvertToBaseUnit(double value);
        double ConvertFromBaseUnit(double baseValue);
        string GetUnitName();

        void ValidateOperationSupport(string operation)
        {
            // By default, allow all operations
        }
    }
}
