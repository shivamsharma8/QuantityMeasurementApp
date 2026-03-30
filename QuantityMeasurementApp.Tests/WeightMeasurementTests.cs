using NUnit.Framework;
using QuantityMeasurementBusinessLayer;
using QuantityMeasurementModelLayer;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class WeightMeasurementTests
    {
        private QuantityMeasurementService _service;

        [SetUp]
        public void Setup()
        {
            _service = new QuantityMeasurementService();
        }

        [Test]
        public void GivenZeroGramAndZeroKilogram_ShouldReturnEqual()
        {
            var first = new Quantity<WeightUnit>(0, WeightUnit.Gram);
            var second = new Quantity<WeightUnit>(0, WeightUnit.Kilogram);

            bool result = _service.AreEqual(first, second);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenOneKilogramAndThousandGram_ShouldReturnEqual()
        {
            var first = new Quantity<WeightUnit>(1, WeightUnit.Kilogram);
            var second = new Quantity<WeightUnit>(1000, WeightUnit.Gram);

            bool result = _service.AreEqual(first, second);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenTwoKilogramAndFiveHundredGram_WhenAdded_ShouldReturnTwoPointFiveKilogram()
        {
            var first = new Quantity<WeightUnit>(2, WeightUnit.Kilogram);
            var second = new Quantity<WeightUnit>(500, WeightUnit.Gram);

            var result = _service.Add(first, second, WeightUnit.Kilogram);

            Assert.That(result.Value, Is.EqualTo(2.5).Within(0.01));
            Assert.That(result.Unit, Is.EqualTo(WeightUnit.Kilogram));
        }

        [Test]
        public void GivenOneKilogramAndFiveHundredGram_WhenSubtracted_ShouldReturnFiveHundredGram()
        {
            var first = new Quantity<WeightUnit>(1, WeightUnit.Kilogram);
            var second = new Quantity<WeightUnit>(500, WeightUnit.Gram);

            var result = _service.Subtract(first, second, WeightUnit.Gram);

            Assert.That(result.Value, Is.EqualTo(500).Within(0.01));
            Assert.That(result.Unit, Is.EqualTo(WeightUnit.Gram));
        }

        [Test]
        public void GivenOneKilogramAndFiveHundredGram_WhenDivided_ShouldReturnTwo()
        {
            var first = new Quantity<WeightUnit>(1, WeightUnit.Kilogram);
            var second = new Quantity<WeightUnit>(500, WeightUnit.Gram);

            double result = _service.Divide(first, second);

            Assert.That(result, Is.EqualTo(2).Within(0.01));
        }
    }
}