using NUnit.Framework;
using QuantityMeasurementModelLayer;
using QuantityMeasurementBusinessLayer;

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
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void GivenDifferentFeet_WhenCompared_ShouldReturnFalse()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Assert.IsFalse(q1.Equals(q2));
        }

        [Test]
        public void GivenFeetAndEquivalentInches_WhenCompared_ShouldReturnTrue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void GivenNull_WhenCompared_ShouldReturnFalse()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Assert.IsFalse(_service.AreEqual(q1, null));
        }

        [Test]
        public void GivenSameReference_WhenCompared_ShouldReturnTrue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Assert.IsTrue(_service.AreEqual(q1, q1));
        }

        [Test]
        public void GivenInvalidUnit_ShouldThrowException()
        {
            Assert.Throws<System.ArgumentException>(() => new Quantity<LengthUnit>(1.0, (LengthUnit)999));
        }
    }
}