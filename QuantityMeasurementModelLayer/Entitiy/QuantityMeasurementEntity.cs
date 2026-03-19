using System;

namespace QuantityMeasurementModelLayer
{
    public class QuantityMeasurementEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Category { get; set; } = string.Empty;

        public double Value1 { get; set; }

        public string Unit1 { get; set; } = string.Empty;

        public double Value2 { get; set; }

        public string Unit2 { get; set; } = string.Empty;

        public string TargetUnit { get; set; } = string.Empty;

        public string AdditionResult { get; set; } = string.Empty;

        public string SubtractionResult { get; set; } = string.Empty;

        public double DivisionResult { get; set; }

        public bool IsEqual { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}