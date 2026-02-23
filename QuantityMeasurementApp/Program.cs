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

            // Feet equality check
            Console.Write("Enter first value in feet: ");
            string feetInput1 = Console.ReadLine();
            Console.Write("Enter second value in feet: ");
            string feetInput2 = Console.ReadLine();

            if (double.TryParse(feetInput1, out double feetValue1) && double.TryParse(feetInput2, out double feetValue2))
            {
                Feet feet1 = new Feet(feetValue1);
                Feet feet2 = new Feet(feetValue2);
                bool feetResult = service.AreEqual(feet1, feet2);
                Console.WriteLine($"Feet: {(feetResult ? "Equal (true)" : "Not Equal (false)")}");
            }
            else
            {
                Console.WriteLine("Invalid input for feet. Please enter numeric values.");
            }

            // Inches equality check
            Console.Write("Enter first value in inches: ");
            string inchInput1 = Console.ReadLine();
            Console.Write("Enter second value in inches: ");
            string inchInput2 = Console.ReadLine();

            if (double.TryParse(inchInput1, out double inchValue1) && double.TryParse(inchInput2, out double inchValue2))
            {
                Inches inch1 = new Inches(inchValue1);
                Inches inch2 = new Inches(inchValue2);
                bool inchResult = service.AreEqual(inch1, inch2);
                Console.WriteLine($"Inches: {(inchResult ? "Equal (true)" : "Not Equal (false)")}");
            }
            else
            {
                Console.WriteLine("Invalid input for inches. Please enter numeric values.");
            }
        }
    }
}