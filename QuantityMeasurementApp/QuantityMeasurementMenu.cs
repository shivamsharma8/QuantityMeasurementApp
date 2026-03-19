using System;
using QuantityMeasurementModelLayer;

namespace QuantityMeasurementApp
{
    public class QuantityMeasurementMenu
    {
        public static void ShowMenu(QuantityMeasurementController controller)
        {
            Console.Write("Choose category (length/weight/volume): ");
            string category = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;

            Console.Write("Enter first value: ");
            string valueInput1 = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter first unit: ");
            string unitInput1 = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Enter second value: ");
            string valueInput2 = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter second unit: ");
            string unitInput2 = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Enter target unit for addition/subtraction result: ");
            string targetUnitInput = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!double.TryParse(valueInput1, out double value1) || !double.TryParse(valueInput2, out double value2))
            {
                Console.WriteLine("Invalid numeric input. Please enter valid numbers.");
                return;
            }

            MeasurementRequestDTO request = new MeasurementRequestDTO
            {
                Category = category,
                Value1 = value1,
                Unit1 = unitInput1,
                Value2 = value2,
                Unit2 = unitInput2,
                TargetUnit = targetUnitInput
            };

            MeasurementResponseDTO response = controller.ProcessMeasurement(request);

            if (!response.IsSuccess)
            {
                Console.WriteLine($"Error: {response.Message}");
                return;
            }

            Console.WriteLine($"\nCategory: {response.Category}");
            Console.WriteLine($"Addition Result: {response.AdditionResult}");
            Console.WriteLine($"Subtraction Result: {response.SubtractionResult}");
            Console.WriteLine($"Division Result: {response.DivisionResult}");
            Console.WriteLine(response.IsEqual ? "Equal (true)" : "Not Equal (false)");
            Console.WriteLine(response.Message);
        }
    }
}