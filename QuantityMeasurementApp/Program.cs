using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            QuantityMeasurementService service = new QuantityMeasurementService();

            Console.Write("Enter first value: ");
            string valueInput1 = Console.ReadLine();
            Console.Write("Enter first unit (feet/inch): ");
            string unitInput1 = Console.ReadLine();

            Console.Write("Enter second value: ");
            string valueInput2 = Console.ReadLine();
            Console.Write("Enter second unit (feet/inch): ");
            string unitInput2 = Console.ReadLine();

            if (double.TryParse(valueInput1, out double value1) && double.TryParse(valueInput2, out double value2)
                && Enum.TryParse(typeof(LengthUnit), Capitalize(unitInput1), out var unit1Obj)
                && Enum.TryParse(typeof(LengthUnit), Capitalize(unitInput2), out var unit2Obj))
            {
                var unit1 = (LengthUnit)unit1Obj;
                var unit2 = (LengthUnit)unit2Obj;
                QuantityLength q1 = new QuantityLength(value1, unit1);
                QuantityLength q2 = new QuantityLength(value2, unit2);
                bool result = service.AreEqual(q1, q2);
                Console.WriteLine(result ? "Equal (true)" : "Not Equal (false)");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter numeric values and valid units (feet/inch).");
            }
        }

        static string Capitalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;
            input = input.Trim().ToLower();
            if (input == "feet" || input == "foot") return "Feet";
            if (input == "inch" || input == "inches") return "Inch";
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}