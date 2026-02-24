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
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsTrue(_service.AreEqual(q1, q2));
        }

        [Test]
        public void GivenDifferentFeet_WhenCompared_ShouldReturnFalse()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(2.0, LengthUnit.Feet);
            Assert.IsFalse(_service.AreEqual(q1, q2));
        }

        [Test]
        public void GivenFeetAndEquivalentInches_WhenCompared_ShouldReturnTrue()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            Assert.IsTrue(_service.AreEqual(q1, q2));
        }

        [Test]
        public void GivenNull_WhenCompared_ShouldReturnFalse()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsFalse(_service.AreEqual(q1, null));
        }

        [Test]
        public void GivenSameReference_WhenCompared_ShouldReturnTrue()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsTrue(_service.AreEqual(q1, q1));
        }

        [Test]
        public void GivenInvalidUnit_ShouldThrowException()
        {
            Assert.Throws<System.ArgumentException>(() => new QuantityLength(1.0, (LengthUnit)999));
        }
    }
}