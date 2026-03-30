using QuantityMeasurementModelLayer.DTO;

namespace QuantityMeasurementBusinessLayer.Interfaces
{
    public interface IQuantityMeasurementService
    {
        QuantityMeasurementDto ProcessAdd(QuantityInputDto request);
        QuantityMeasurementDto ProcessSubtract(QuantityInputDto request);
        QuantityMeasurementDto ProcessMultiply(QuantityInputDto request);
        QuantityMeasurementDto ProcessDivide(QuantityInputDto request);
        QuantityMeasurementDto ProcessCompare(QuantityInputDto request);
        QuantityMeasurementDto ProcessConvert(QuantityInputDto request);
    }
}
