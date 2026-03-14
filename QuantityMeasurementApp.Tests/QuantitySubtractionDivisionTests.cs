using System;
using QuantityMeasurementModelLayer;
using QuantityMeasurementBusinessLayer;
using Xunit;

namespace QuantityMeasurementApp.Tests
{
    public class QuantitySubtractionDivisionTests
    {
        [Fact]
        public void Subtract_LitreMinusMillilitre_Equivalent()
        {
            var q1 = new Quantity<VolumeUnit>(2.0, VolumeUnit.LITRE);
            var q2 = new Quantity<VolumeUnit>(500.0, VolumeUnit.MILLILITRE);
            var diff = q1.Subtract(q2);
            Assert.Equal(1.5, diff.Value, 2);
            Assert.Equal(VolumeUnit.LITRE, diff.Unit);
        }

        [Fact]
        public void Subtract_ExplicitTargetUnit_Millilitre()
        {
            var q1 = new Quantity<VolumeUnit>(2.0, VolumeUnit.LITRE);
            var q2 = new Quantity<VolumeUnit>(500.0, VolumeUnit.MILLILITRE);
            var diff = q1.Subtract(q2, VolumeUnit.MILLILITRE);
            Assert.Equal(1500.0, diff.Value, 2);
            Assert.Equal(VolumeUnit.MILLILITRE, diff.Unit);
        }

        [Fact]
        public void Divide_KilogramByGram()
        {
            var q1 = new Quantity<WeightUnit>(2.0, WeightUnit.Kilogram);
            var q2 = new Quantity<WeightUnit>(500.0, WeightUnit.Gram);
            var ratio = q1.Divide(q2);
            Assert.Equal(4.0, ratio, 2);
        }

        [Fact]
        public void Divide_LengthByLength()
        {
            var q1 = new Quantity<LengthUnit>(36.0, LengthUnit.Inch);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Yard);
            var ratio = q1.Divide(q2);
            Assert.Equal(1.0, ratio, 2);
        }

        [Fact]
        public void Subtract_DifferentCategories_Throws()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Assert.Throws<ArgumentException>(() => ((dynamic)q1).Subtract(q2));
        }

        [Fact]
        public void Divide_ByZero_Throws()
        {
            var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
            var q2 = new Quantity<VolumeUnit>(0.0, VolumeUnit.LITRE);
            Assert.Throws<DivideByZeroException>(() => q1.Divide(q2));
        }
    }
}
