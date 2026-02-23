using NUnit.Framework;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityMeasurementServiceTests
    {
        private QuantityMeasurementService _service;

        [SetUp]
        public void Setup()
        {
            _service = new QuantityMeasurementService();
        }

        [Test]
        public void GivenSameFeet_WhenCompared_ShouldReturnTrue()
        {
            var f1 = new Feet(1.0);
            var f2 = new Feet(1.0);

            Assert.IsTrue(_service.AreEqual(f1, f2));
        }

        [Test]
        public void GivenDifferentFeet_WhenCompared_ShouldReturnFalse()
        {
            var f1 = new Feet(1.0);
            var f2 = new Feet(2.0);

            Assert.IsFalse(_service.AreEqual(f1, f2));
        }

        [Test]
        public void GivenNull_WhenCompared_ShouldReturnFalse()
        {
            var f1 = new Feet(1.0);

            Assert.IsFalse(_service.AreEqual(f1, null));
        }
    }
}