using NUnit.Framework;
using QuantityMeasurementModelLayer;
using QuantityMeasurementBusinessLayer;

namespace QuantityMeasurementApp.Tests
{
    public class YardAndCentimeterTests
    {
        private QuantityMeasurementService _service;

        [SetUp]
        public void Setup()
        {
            _service = new QuantityMeasurementService();
        }

        [Test]
        public void GivenSameYard_WhenCompared_ShouldReturnTrue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Yard);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Yard);
            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void GivenDifferentYard_WhenCompared_ShouldReturnFalse()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Yard);
            var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.Yard);
            Assert.IsFalse(q1.Equals(q2));
        }

        [Test]
        public void GivenYardAndEquivalentFeet_WhenCompared_ShouldReturnTrue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Yard);
            var q2 = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);
            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void GivenYardAndEquivalentInches_WhenCompared_ShouldReturnTrue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Yard);
            var q2 = new Quantity<LengthUnit>(36.0, LengthUnit.Inch);
            Assert.IsTrue(_service.AreEqual(q1, q2));
        }

        [Test]
        public void GivenCentimeterAndEquivalentInch_WhenCompared_ShouldReturnTrue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Centimeter);
            var q2 = new Quantity<LengthUnit>(0.393701, LengthUnit.Inch);
            Assert.IsTrue(_service.AreEqual(q1, q2));
        }

        [Test]
        public void GivenCentimeterAndEquivalentFeet_WhenCompared_ShouldReturnFalse()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Centimeter);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Assert.IsFalse(_service.AreEqual(q1, q2));
        }

        [Test]
        public void GivenMultiUnitTransitiveProperty_ShouldReturnTrue()
        {
            var q1 = new Quantity<LengthUnit>(2.0, LengthUnit.Yard);
            var q2 = new Quantity<LengthUnit>(6.0, LengthUnit.Feet);
            var q3 = new Quantity<LengthUnit>(72.0, LengthUnit.Inch);
            Assert.IsTrue(_service.AreEqual(q1, q2));
            Assert.IsTrue(_service.AreEqual(q2, q3));
            Assert.IsTrue(_service.AreEqual(q1, q3));
        }

        [Test]
        public void GivenCentimeterSameReference_WhenCompared_ShouldReturnTrue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Centimeter);
            Assert.IsTrue(_service.AreEqual(q1, q1));
        }

        [Test]
        public void GivenCentimeterNullComparison_WhenCompared_ShouldReturnFalse()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Centimeter);
            Assert.IsFalse(_service.AreEqual(q1, null));
        }
    }
}
