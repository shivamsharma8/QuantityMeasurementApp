using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementModelLayer.DTO
{
    /// <summary>
    /// Representation of a cross-unit measurement request.
    /// </summary>
    public class QuantityInputDto
    {
        /// <summary>
        /// The dimension of measurement. 
        /// Valid ranges: "length", "weight", "volume", "temperature"
        /// </summary>
        [Required]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// The value to process.
        /// </summary>
        [Required]
        public double Value1 { get; set; }

        /// <summary>
        /// The unit of Value1. 
        /// length: inch, feet, yard, centimeter | weight: gram, kg, tonne | volume: ml, litre, gallon | temperature: c, f
        /// </summary>
        [Required]
        public string Unit1 { get; set; } = string.Empty;

        /// <summary>
        /// Optional second value to process (for Addition/Subtraction).
        /// </summary>
        public double? Value2 { get; set; }

        /// <summary>
        /// The unit of Value2. Must be in the same dimension category as Unit1.
        /// </summary>
        public string? Unit2 { get; set; }

        /// <summary>
        /// Target unit to convert the result into. Must be precisely one of the specific category units.
        /// </summary>
        public string? TargetUnit { get; set; }
    }
}
