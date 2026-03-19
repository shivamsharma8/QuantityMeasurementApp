using NUnit.Framework;
using QuantityMeasurementBusinessLayer;
using QuantityMeasurementModelLayer;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class TemperatureMeasurementTests
    {
        private QuantityMeasurementService _service;

        [SetUp]
        public void Setup()
        {
            _service = new QuantityMeasurementService();
        }

        [Test]
        public void GivenZeroCelsiusAndThirtyTwoFahrenheit_ShouldReturnEqual()
        {
            var first = new Quantity<TemperatureUnit>(0, TemperatureUnit.CELSIUS);
            var second = new Quantity<TemperatureUnit>(32, TemperatureUnit.FAHRENHEIT);

            bool result = _service.AreEqual(first, second);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenHundredCelsiusAndTwoHundredTwelveFahrenheit_ShouldReturnEqual()
        {
            var first = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
            var second = new Quantity<TemperatureUnit>(212, TemperatureUnit.FAHRENHEIT);

            bool result = _service.AreEqual(first, second);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenZeroCelsius_WhenConvertedToFahrenheit_ShouldReturnThirtyTwo()
        {
            var quantity = new Quantity<TemperatureUnit>(0, TemperatureUnit.CELSIUS);

            var result = _service.Convert(quantity, TemperatureUnit.FAHRENHEIT);

            Assert.That(result.Value, Is.EqualTo(32).Within(0.01));
            Assert.That(result.Unit, Is.EqualTo(TemperatureUnit.FAHRENHEIT));
        }

        [Test]
        public void GivenThirtyTwoFahrenheit_WhenConvertedToCelsius_ShouldReturnZero()
        {
            var quantity = new Quantity<TemperatureUnit>(32, TemperatureUnit.FAHRENHEIT);

            var result = _service.Convert(quantity, TemperatureUnit.CELSIUS);

            Assert.That(result.Value, Is.EqualTo(0).Within(0.01));
            Assert.That(result.Unit, Is.EqualTo(TemperatureUnit.CELSIUS));
        }

        [Test]
        public void GivenTenCelsiusAndTenFahrenheit_ShouldReturnNotEqual()
        {
            var first = new Quantity<TemperatureUnit>(10, TemperatureUnit.CELSIUS);
            var second = new Quantity<TemperatureUnit>(10, TemperatureUnit.FAHRENHEIT);

            bool result = _service.AreEqual(first, second);

            Assert.That(result, Is.False);
        }
    }
}