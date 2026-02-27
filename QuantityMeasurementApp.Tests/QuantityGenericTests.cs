using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityGenericTests
    {
        [Test]
        public void Length_Equality_CrossUnit()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void Length_Conversion()
        {
            var q = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var result = q.ConvertTo(LengthUnit.Inch);
            Assert.AreEqual(12.0, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Inch, result.Unit);
        }

        [Test]
        public void Length_Addition_CrossUnit()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            var sum = Quantity<LengthUnit>.Add(q1, q2, LengthUnit.Feet);
            Assert.AreEqual(2.0, sum.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Feet, sum.Unit);
        }

        [Test]
        public void Weight_Equality_CrossUnit()
        {
            var q1 = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            var q2 = new Quantity<WeightUnit>(1000.0, WeightUnit.Gram);
            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void Weight_Conversion()
        {
            var q = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            var result = q.ConvertTo(WeightUnit.Gram);
            Assert.AreEqual(1000.0, result.Value, 1e-6);
            Assert.AreEqual(WeightUnit.Gram, result.Unit);
        }

        [Test]
        public void Weight_Addition_CrossUnit()
        {
            var q1 = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            var q2 = new Quantity<WeightUnit>(1000.0, WeightUnit.Gram);
            var sum = Quantity<WeightUnit>.Add(q1, q2, WeightUnit.Kilogram);
            Assert.AreEqual(2.0, sum.Value, 1e-6);
            Assert.AreEqual(WeightUnit.Kilogram, sum.Unit);
        }

        [Test]
        public void CrossCategory_Equality_ShouldBeFalse()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Assert.IsFalse(q1.Equals(q2));
        }
    }
}
