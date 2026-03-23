using System;
using QuantityMeasurementModelLayer;

namespace QuantityMeasurementApp
{
    public class QuantityMeasurementMenu
    {
        public static void ShowMenu(QuantityMeasurementController controller)
        {
            Console.Write("Choose category (length/weight/volume/temperature): ");
            string category = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;

            string availableUnits = GetAvailableUnitsForCategory(category);
            if (string.IsNullOrEmpty(availableUnits))
            {
                Console.WriteLine("Invalid category. Supported categories: length, weight, volume, temperature.");
                return;
            }

            Console.WriteLine($"Available units for {category}: {availableUnits}");

            Console.Write("Enter first value: ");
            string valueInput1 = Console.ReadLine() ?? string.Empty;

            Console.Write($"Enter first unit ({availableUnits}): ");
            string unitInput1 = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Enter second value: ");
            string valueInput2 = Console.ReadLine() ?? string.Empty;

            Console.Write($"Enter second unit ({availableUnits}): ");
            string unitInput2 = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write($"Enter target unit for addition/subtraction result ({availableUnits}): ");
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

        private static string GetAvailableUnitsForCategory(string category)
        {
            switch (category)
            {
                case "length":
                    return "inch, feet (ft), yard, centimeter (cm)";

                case "weight":
                    return "gram (g), kilogram (kg), tonne (t)";

                case "volume":
                    return "millilitre (ml), litre (l), gallon";

                case "temperature":
                    return "celsius (c), fahrenheit (f)";

                default:
                    return string.Empty;
            }
        }
    }
}