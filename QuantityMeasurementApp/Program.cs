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

            Console.Write("Choose category (length/weight): ");
            string category = Console.ReadLine()?.Trim().ToLower();

            if (category == "length")
            {
                Console.Write("Enter first value: ");
                string valueInput1 = Console.ReadLine();
                Console.Write("Enter first unit (feet/inch/yard/cm): ");
                string unitInput1 = Console.ReadLine();

                Console.Write("Enter second value: ");
                string valueInput2 = Console.ReadLine();
                Console.Write("Enter second unit (feet/inch/yard/cm): ");
                string unitInput2 = Console.ReadLine();

                Console.Write("Enter target unit for addition result (feet/inch/yard/cm): ");
                string targetUnitInput = Console.ReadLine();

                if (double.TryParse(valueInput1, out double value1) && double.TryParse(valueInput2, out double value2)
                    && Enum.TryParse(typeof(LengthUnit), Capitalize(unitInput1), out var unit1Obj)
                    && Enum.TryParse(typeof(LengthUnit), Capitalize(unitInput2), out var unit2Obj)
                    && Enum.TryParse(typeof(LengthUnit), Capitalize(targetUnitInput), out var targetUnitObj))
                {
                    var unit1 = (LengthUnit)unit1Obj;
                    var unit2 = (LengthUnit)unit2Obj;
                    var targetUnit = (LengthUnit)targetUnitObj;
                    QuantityLength q1 = new QuantityLength(value1, unit1);
                    QuantityLength q2 = new QuantityLength(value2, unit2);
                    var sum = QuantityLength.Add(q1, q2, targetUnit);
                    Console.WriteLine($"Addition Result: {sum.Value} {sum.Unit}");
                    bool result = service.AreEqual(q1, q2);
                    Console.WriteLine(result ? "Equal (true)" : "Not Equal (false)");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter numeric values and valid units (feet/inch/yard/cm). Also specify a valid target unit.");
                }
            }
            else if (category == "weight")
            {
                Console.Write("Enter first value: ");
                string valueInput1 = Console.ReadLine();
                Console.Write("Enter first unit (kg/g/lb): ");
                string unitInput1 = Console.ReadLine();

                Console.Write("Enter second value: ");
                string valueInput2 = Console.ReadLine();
                Console.Write("Enter second unit (kg/g/lb): ");
                string unitInput2 = Console.ReadLine();

                Console.Write("Enter target unit for addition result (kg/g/lb): ");
                string targetUnitInput = Console.ReadLine();

                if (double.TryParse(valueInput1, out double value1) && double.TryParse(valueInput2, out double value2)
                    && Enum.TryParse(typeof(WeightUnit), CapitalizeWeight(unitInput1), out var unit1Obj)
                    && Enum.TryParse(typeof(WeightUnit), CapitalizeWeight(unitInput2), out var unit2Obj)
                    && Enum.TryParse(typeof(WeightUnit), CapitalizeWeight(targetUnitInput), out var targetUnitObj))
                {
                    var unit1 = (WeightUnit)unit1Obj;
                    var unit2 = (WeightUnit)unit2Obj;
                    var targetUnit = (WeightUnit)targetUnitObj;
                    QuantityWeight q1 = new QuantityWeight(value1, unit1);
                    QuantityWeight q2 = new QuantityWeight(value2, unit2);
                    var sum = QuantityWeight.Add(q1, q2, targetUnit);
                    Console.WriteLine($"Addition Result: {sum.Value} {sum.Unit}");
                    bool result = q1.Equals(q2);
                    Console.WriteLine(result ? "Equal (true)" : "Not Equal (false)");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter numeric values and valid units (kg/g/lb). Also specify a valid target unit.");
                }
            }
            else
            {
                Console.WriteLine("Invalid category. Please enter 'length' or 'weight'.");
            }
                static string CapitalizeWeight(string input)
                {
                    if (string.IsNullOrWhiteSpace(input)) return input;
                    input = input.Trim().ToLower();
                    if (input == "kg" || input == "kilogram" || input == "kilograms") return "Kilogram";
                    if (input == "g" || input == "gram" || input == "grams") return "Gram";
                    if (input == "lb" || input == "pound" || input == "pounds") return "Pound";
                    return char.ToUpper(input[0]) + input.Substring(1);
                }
        }

        static string Capitalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;
            input = input.Trim().ToLower();
            if (input == "feet" || input == "foot") return "Feet";
            if (input == "inch" || input == "inches") return "Inch";
            if (input == "yard" || input == "yards") return "Yard";
            if (input == "cm" || input == "centimeter" || input == "centimeters") return "Centimeter";
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}