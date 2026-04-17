namespace QuantityMeasurementModelLayer
{
    public class MeasurementResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string Category { get; set; } = string.Empty;
        public string AdditionResult { get; set; } = string.Empty;
        public string SubtractionResult { get; set; } = string.Empty;
        public double DivisionResult { get; set; }
        public bool IsEqual { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}