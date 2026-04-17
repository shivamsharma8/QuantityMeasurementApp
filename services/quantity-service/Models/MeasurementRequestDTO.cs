namespace QuantityMeasurementModelLayer
{
    public class MeasurementRequestDTO
    {
        public string Category { get; set; }
        public double Value1 { get; set; }
        public string Unit1 { get; set; }
        public double Value2 { get; set; }
        public string Unit2 { get; set; }
        public string TargetUnit { get; set; }
    }
}