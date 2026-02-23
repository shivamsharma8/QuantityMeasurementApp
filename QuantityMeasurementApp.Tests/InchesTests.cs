using NUnit.Framework;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

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
            var i1 = new Inches(1.0);
            var i2 = new Inches(1.0);
            Assert.IsTrue(_service.AreEqual(i1, i2));
        }

        [Test]
        public void GivenDifferentInches_WhenCompared_ShouldReturnFalse()
        {
            var i1 = new Inches(1.0);
            var i2 = new Inches(2.0);
            Assert.IsFalse(_service.AreEqual(i1, i2));
        }

        [Test]
        public void GivenNull_WhenCompared_ShouldReturnFalse()
        {
            var i1 = new Inches(1.0);
            Assert.IsFalse(_service.AreEqual(i1, null));
        }

        [Test]
        public void GivenSameReference_WhenCompared_ShouldReturnTrue()
        {
            var i1 = new Inches(1.0);
            Assert.IsTrue(_service.AreEqual(i1, i1));
        }
    }
}
