using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter first value in feet: ");
            string input1 = Console.ReadLine();

            Console.Write("Enter second value in feet: ");
            string input2 = Console.ReadLine();

            if (double.TryParse(input1, out double value1) &&
                double.TryParse(input2, out double value2))
            {
                Feet feet1 = new Feet(value1);
                Feet feet2 = new Feet(value2);

                QuantityMeasurementService service = new QuantityMeasurementService();

                bool result = service.AreEqual(feet1, feet2);

                Console.WriteLine(result
                    ? "Equal (true)"
                    : "Not Equal (false)");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter numeric values.");
            }
        }
    }
}