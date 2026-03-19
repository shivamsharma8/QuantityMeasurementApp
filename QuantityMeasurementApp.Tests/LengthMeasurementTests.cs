using NUnit.Framework;
using QuantityMeasurementBusinessLayer;
using QuantityMeasurementModelLayer;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class LengthMeasurementTests
    {
        private QuantityMeasurementService _service;

        [SetUp]
        public void Setup()
        {
            _service = new QuantityMeasurementService();
        }

        [Test]
        public void GivenZeroFeetAndZeroInch_ShouldReturnEqual()
        {
            var first = new Quantity<LengthUnit>(0, LengthUnit.Feet);
            var second = new Quantity<LengthUnit>(0, LengthUnit.Inch);

            bool result = _service.AreEqual(first, second);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenOneFeetAndTwelveInch_ShouldReturnEqual()
        {
            var first = new Quantity<LengthUnit>(1, LengthUnit.Feet);
            var second = new Quantity<LengthUnit>(12, LengthUnit.Inch);

            bool result = _service.AreEqual(first, second);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenOneInchAndOneFeet_ShouldReturnNotEqual()
        {
            var first = new Quantity<LengthUnit>(1, LengthUnit.Inch);
            var second = new Quantity<LengthUnit>(1, LengthUnit.Feet);

            bool result = _service.AreEqual(first, second);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenTwoInchAndFiveCm_WhenAdded_ShouldReturnSevenCm()
        {
            var first = new Quantity<LengthUnit>(2, LengthUnit.Inch);
            var second = new Quantity<LengthUnit>(5, LengthUnit.Centimeter);

            var result = _service.Add(first, second, LengthUnit.Centimeter);

            Assert.That(result.Value, Is.EqualTo(10.08).Within(0.01));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.Centimeter));
        }

        [Test]
        public void GivenOneFeetAndSixInch_WhenSubtracted_ShouldReturnSixInch()
        {
            var first = new Quantity<LengthUnit>(1, LengthUnit.Feet);
            var second = new Quantity<LengthUnit>(6, LengthUnit.Inch);

            var result = _service.Subtract(first, second, LengthUnit.Inch);

            Assert.That(result.Value, Is.EqualTo(6).Within(0.01));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.Inch));
        }

        [Test]
        public void GivenOneFeetAndTwelveInch_WhenDivided_ShouldReturnOne()
        {
            var first = new Quantity<LengthUnit>(1, LengthUnit.Feet);
            var second = new Quantity<LengthUnit>(12, LengthUnit.Inch);

            double result = _service.Divide(first, second);

            Assert.That(result, Is.EqualTo(1).Within(0.01));
        }
    }
}