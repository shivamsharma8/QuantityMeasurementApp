namespace QuantityMeasurementModelLayer.DTO
{
    public class QuantityMeasurementDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        
        // Single operation result info
        public string OperationType { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public bool IsEqual { get; set; }
        public double DivisionResult { get; set; }
    }
}
