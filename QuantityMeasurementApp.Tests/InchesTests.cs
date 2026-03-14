using NUnit.Framework;
using QuantityMeasurementModelLayer;
using QuantityMeasurementBusinessLayer;

namespace QuantityMeasurementApp.Tests
{
    public class InchesTests
    {
        private QuantityMeasurementService _service;

        [SetUp]
        public void Setup()
        {
            _service = new QuantityMeasurementService();
        }

        [Test]
        public void GivenSameInches_WhenCompared_ShouldReturnTrue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Inch);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Inch);
            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void GivenDifferentInches_WhenCompared_ShouldReturnFalse()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Inch);
            var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.Inch);
            Assert.IsFalse(q1.Equals(q2));
        }

        [Test]
        public void GivenInchesAndEquivalentFeet_WhenCompared_ShouldReturnTrue()
        {
            var q1 = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void GivenNull_WhenCompared_ShouldReturnFalse()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Inch);
            Assert.IsFalse(_service.AreEqual(q1, null));
        }

        [Test]
        public void GivenSameReference_WhenCompared_ShouldReturnTrue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Inch);
            Assert.IsTrue(_service.AreEqual(q1, q1));
        }

        [Test]
        public void GivenInvalidUnit_ShouldThrowException()
        {
            Assert.Throws<System.ArgumentException>(() => new Quantity<LengthUnit>(1.0, (LengthUnit)999));
        }
    }
}
