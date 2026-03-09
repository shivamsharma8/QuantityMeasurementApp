using QuantityMeasurementApp.Models;
using Xunit;

namespace QuantityMeasurementApp.Tests
{
    public class VolumeUnitTests
    {
        [Fact]
        public void testEquality_LitreToLitre_SameValue()
        {
            var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
            var q2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
            Assert.True(q1.Equals(q2));
        }

        [Fact]
        public void testEquality_LitreToMillilitre_EquivalentValue()
        {
            var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
            var q2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);
            Assert.True(q1.Equals(q2));
        }

        [Fact]
        public void testEquality_LitreToGallon_EquivalentValue()
        {
            var q1 = new Quantity<VolumeUnit>(3.78541, VolumeUnit.LITRE);
            var q2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.GALLON);
            Assert.True(q1.Equals(q2));
        }

        [Fact]
        public void testConversion_LitreToMillilitre()
        {
            var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
            var q2 = q1.ConvertTo(VolumeUnit.MILLILITRE);
            Assert.Equal(1000.0, q2.Value, 2);
            Assert.Equal(VolumeUnit.MILLILITRE, q2.Unit);
        }

        [Fact]
        public void testAddition_SameUnit_LitrePlusLitre()
        {
            var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
            var q2 = new Quantity<VolumeUnit>(2.0, VolumeUnit.LITRE);
            var sum = q1.Add(q2);
            Assert.Equal(3.0, sum.Value, 2);
            Assert.Equal(VolumeUnit.LITRE, sum.Unit);
        }

        [Fact]
        public void testAddition_CrossUnit_LitrePlusMillilitre()
        {
            var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
            var q2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);
            var sum = q1.Add(q2);
            Assert.Equal(2.0, sum.Value, 2);
            Assert.Equal(VolumeUnit.LITRE, sum.Unit);
        }

        [Fact]
        public void testAddition_ExplicitTargetUnit_Millilitre()
        {
            var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
            var q2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);
            var sum = q1.Add(q2, VolumeUnit.MILLILITRE);
            Assert.Equal(2000.0, sum.Value, 2);
            Assert.Equal(VolumeUnit.MILLILITRE, sum.Unit);
        }
    }
}
