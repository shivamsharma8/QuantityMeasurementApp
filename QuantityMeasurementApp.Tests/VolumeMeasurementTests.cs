using NUnit.Framework;
using QuantityMeasurementBusinessLayer;
using QuantityMeasurementModelLayer;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class VolumeMeasurementTests
    {
        private QuantityMeasurementService _service;

        [SetUp]
        public void Setup()
        {
            _service = new QuantityMeasurementService();
        }

        [Test]
        public void GivenOneLitreAndThousandMilliLitre_ShouldReturnEqual()
        {
            var first = new Quantity<VolumeUnit>(1, VolumeUnit.LITRE);
            var second = new Quantity<VolumeUnit>(1000, VolumeUnit.MILLILITRE);

            bool result = _service.AreEqual(first, second);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenTwoLitreAndFiveHundredMilliLitre_WhenAdded_ShouldReturnTwoPointFiveLitre()
        {
            var first = new Quantity<VolumeUnit>(2, VolumeUnit.LITRE);
            var second = new Quantity<VolumeUnit>(500, VolumeUnit.MILLILITRE);

            var result = _service.Add(first, second, VolumeUnit.LITRE);

            Assert.That(result.Value, Is.EqualTo(2.5).Within(0.01));
            Assert.That(result.Unit, Is.EqualTo(VolumeUnit.LITRE));
        }

        [Test]
        public void GivenOneLitreAndFiveHundredMilliLitre_WhenSubtracted_ShouldReturnFiveHundredMilliLitre()
        {
            var first = new Quantity<VolumeUnit>(1, VolumeUnit.LITRE);
            var second = new Quantity<VolumeUnit>(500, VolumeUnit.MILLILITRE);

            var result = _service.Subtract(first, second, VolumeUnit.MILLILITRE);

            Assert.That(result.Value, Is.EqualTo(500).Within(0.01));
            Assert.That(result.Unit, Is.EqualTo(VolumeUnit.MILLILITRE));
        }

        [Test]
        public void GivenOneLitreAndFiveHundredMilliLitre_WhenDivided_ShouldReturnTwo()
        {
            var first = new Quantity<VolumeUnit>(1, VolumeUnit.LITRE);
            var second = new Quantity<VolumeUnit>(500, VolumeUnit.MILLILITRE);

            double result = _service.Divide(first, second);

            Assert.That(result, Is.EqualTo(2).Within(0.01));
        }

        [Test]
        public void GivenOneGallonAndThreePointSevenEightLitre_ShouldReturnApproximatelyEqual()
        {
            var first = new Quantity<VolumeUnit>(1, VolumeUnit.GALLON);
            var second = new Quantity<VolumeUnit>(3.78, VolumeUnit.LITRE);

            bool result = _service.AreEqual(first, second);

            Assert.That(result, Is.True);
        }
    }
}