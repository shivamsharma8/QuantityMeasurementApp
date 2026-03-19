using QuantityMeasurementBusinessLayer;
using QuantityMeasurementModelLayer;

namespace QuantityMeasurementApp
{
    public class QuantityMeasurementController
    {
        private readonly QuantityMeasurementService _service;

        public QuantityMeasurementController(QuantityMeasurementService service)
        {
            _service = service;
        }

        public MeasurementResponseDTO ProcessMeasurement(MeasurementRequestDTO request)
        {
            return _service.ProcessMeasurement(request);
        }
    }
}