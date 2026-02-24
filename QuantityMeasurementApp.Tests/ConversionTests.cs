using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class ConversionTests
    {
        [Test]
        public void TestConversion_FeetToInches()
        {
            double result = QuantityLength.Convert(1.0, LengthUnit.Feet, LengthUnit.Inch);
            Assert.AreEqual(12.0, result, 1e-6);
        }

        [Test]
        public void TestConversion_InchesToFeet()
        {
            double result = QuantityLength.Convert(24.0, LengthUnit.Inch, LengthUnit.Feet);
            Assert.AreEqual(2.0, result, 1e-6);
        }

        [Test]
        public void TestConversion_YardsToInches()
        {
            double result = QuantityLength.Convert(1.0, LengthUnit.Yard, LengthUnit.Inch);
            Assert.AreEqual(36.0, result, 1e-6);
        }

        [Test]
        public void TestConversion_InchesToYards()
        {
            double result = QuantityLength.Convert(72.0, LengthUnit.Inch, LengthUnit.Yard);
            Assert.AreEqual(2.0, result, 1e-6);
        }

        [Test]
        public void TestConversion_CentimetersToInches()
        {
            double result = QuantityLength.Convert(2.54, LengthUnit.Centimeter, LengthUnit.Inch);
            Assert.AreEqual(1.0, result, 1e-6);
        }

        [Test]
        public void TestConversion_FeetToYard()
        {
            double result = QuantityLength.Convert(6.0, LengthUnit.Feet, LengthUnit.Yard);
            Assert.AreEqual(2.0, result, 1e-6);
        }

        [Test]
        public void TestConversion_ZeroValue()
        {
            double result = QuantityLength.Convert(0.0, LengthUnit.Feet, LengthUnit.Inch);
            Assert.AreEqual(0.0, result, 1e-6);
        }

        [Test]
        public void TestConversion_NegativeValue()
        {
            double result = QuantityLength.Convert(-1.0, LengthUnit.Feet, LengthUnit.Inch);
            Assert.AreEqual(-12.0, result, 1e-6);
        }

        [Test]
        public void TestConversion_SameUnit()
        {
            double result = QuantityLength.Convert(5.0, LengthUnit.Feet, LengthUnit.Feet);
            Assert.AreEqual(5.0, result, 1e-6);
        }

        [Test]
        public void TestConversion_InvalidUnit_Throws()
        {
            Assert.Throws<System.ArgumentException>(() => QuantityLength.Convert(1.0, (LengthUnit)999, LengthUnit.Feet));
            Assert.Throws<System.ArgumentException>(() => QuantityLength.Convert(1.0, LengthUnit.Feet, (LengthUnit)999));
        }

        [Test]
        public void TestConversion_NaNOrInfinite_Throws()
        {
            Assert.Throws<System.ArgumentException>(() => QuantityLength.Convert(double.NaN, LengthUnit.Feet, LengthUnit.Inch));
            Assert.Throws<System.ArgumentException>(() => QuantityLength.Convert(double.PositiveInfinity, LengthUnit.Feet, LengthUnit.Inch));
            Assert.Throws<System.ArgumentException>(() => QuantityLength.Convert(double.NegativeInfinity, LengthUnit.Feet, LengthUnit.Inch));
        }

        [Test]
        public void TestConversion_RoundTrip_PreservesValue()
        {
            double value = 3.5;
            double inches = QuantityLength.Convert(value, LengthUnit.Feet, LengthUnit.Inch);
            double feet = QuantityLength.Convert(inches, LengthUnit.Inch, LengthUnit.Feet);
            Assert.AreEqual(value, feet, 1e-6);
        }
    }
}
