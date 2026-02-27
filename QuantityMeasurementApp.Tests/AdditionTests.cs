using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class AdditionTests
    {
        [Test]
        public void Addition_SameUnit_FeetPlusFeet()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(2.0, LengthUnit.Feet);
            var result = QuantityLength.Add(q1, q2);
            Assert.AreEqual(3.0, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [Test]
        public void Addition_SameUnit_InchPlusInch()
        {
            var q1 = new QuantityLength(6.0, LengthUnit.Inch);
            var q2 = new QuantityLength(6.0, LengthUnit.Inch);
            var result = QuantityLength.Add(q1, q2);
            Assert.AreEqual(12.0, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Inch, result.Unit);
        }

        [Test]
        public void Addition_CrossUnit_FeetPlusInches()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            var result = QuantityLength.Add(q1, q2);
            Assert.AreEqual(2.0, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [Test]
        public void Addition_CrossUnit_InchPlusFeet()
        {
            var q1 = new QuantityLength(12.0, LengthUnit.Inch);
            var q2 = new QuantityLength(1.0, LengthUnit.Feet);
            var result = QuantityLength.Add(q1, q2);
            Assert.AreEqual(24.0, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Inch, result.Unit);
        }

        [Test]
        public void Addition_CrossUnit_YardPlusFeet()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Yard);
            var q2 = new QuantityLength(3.0, LengthUnit.Feet);
            var result = QuantityLength.Add(q1, q2);
            Assert.AreEqual(2.0, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Yard, result.Unit);
        }

        [Test]
        public void Addition_CrossUnit_CentimeterPlusInch()
        {
            var q1 = new QuantityLength(2.54, LengthUnit.Centimeter);
            var q2 = new QuantityLength(1.0, LengthUnit.Inch);
            var result = QuantityLength.Add(q1, q2);
            Assert.AreEqual(5.08, result.Value, 1e-2); // Allowing epsilon for floating point
            Assert.AreEqual(LengthUnit.Centimeter, result.Unit);
        }

        [Test]
        public void Addition_Commutativity()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            var result1 = QuantityLength.Add(q1, q2);
            var result2 = QuantityLength.Add(q2, q1);
            Assert.AreEqual(result1.Value, QuantityLength.Convert(result2.Value, result2.Unit, result1.Unit), 1e-6);
        }

        [Test]
        public void Addition_WithZero()
        {
            var q1 = new QuantityLength(5.0, LengthUnit.Feet);
            var q2 = new QuantityLength(0.0, LengthUnit.Inch);
            var result = QuantityLength.Add(q1, q2);
            Assert.AreEqual(5.0, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [Test]
        public void Addition_NegativeValues()
        {
            var q1 = new QuantityLength(5.0, LengthUnit.Feet);
            var q2 = new QuantityLength(-2.0, LengthUnit.Feet);
            var result = QuantityLength.Add(q1, q2);
            Assert.AreEqual(3.0, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [Test]
        public void Addition_NullSecondOperand_Throws()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.Throws<System.ArgumentException>(() => QuantityLength.Add(q1, null));
        }

        [Test]
        public void Addition_LargeValues()
        {
            var q1 = new QuantityLength(1e6, LengthUnit.Feet);
            var q2 = new QuantityLength(1e6, LengthUnit.Feet);
            var result = QuantityLength.Add(q1, q2);
            Assert.AreEqual(2e6, result.Value, 1e-2);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [Test]
        public void Addition_SmallValues()
        {
            var q1 = new QuantityLength(0.001, LengthUnit.Feet);
            var q2 = new QuantityLength(0.002, LengthUnit.Feet);
            var result = QuantityLength.Add(q1, q2);
            Assert.AreEqual(0.003, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }
    }
}
