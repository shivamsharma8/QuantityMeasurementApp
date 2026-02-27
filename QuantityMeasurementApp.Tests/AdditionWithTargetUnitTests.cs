using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class AdditionWithTargetUnitTests
    {
        [Test]
        public void Addition_ExplicitTargetUnit_Feet()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            var result = QuantityLength.Add(q1, q2, LengthUnit.Feet);
            Assert.AreEqual(2.0, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [Test]
        public void Addition_ExplicitTargetUnit_Inches()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            var result = QuantityLength.Add(q1, q2, LengthUnit.Inch);
            Assert.AreEqual(24.0, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Inch, result.Unit);
        }

        [Test]
        public void Addition_ExplicitTargetUnit_Yards()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            var result = QuantityLength.Add(q1, q2, LengthUnit.Yard);
            Assert.AreEqual(0.667, result.Value, 1e-3);
            Assert.AreEqual(LengthUnit.Yard, result.Unit);
        }

        [Test]
        public void Addition_ExplicitTargetUnit_Centimeters()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Inch);
            var q2 = new QuantityLength(1.0, LengthUnit.Inch);
            var result = QuantityLength.Add(q1, q2, LengthUnit.Centimeter);
            Assert.AreEqual(5.08, result.Value, 1e-2);
            Assert.AreEqual(LengthUnit.Centimeter, result.Unit);
        }

        [Test]
        public void Addition_ExplicitTargetUnit_SameAsFirstOperand()
        {
            var q1 = new QuantityLength(2.0, LengthUnit.Yard);
            var q2 = new QuantityLength(3.0, LengthUnit.Feet);
            var result = QuantityLength.Add(q1, q2, LengthUnit.Yard);
            Assert.AreEqual(3.0, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Yard, result.Unit);
        }

        [Test]
        public void Addition_ExplicitTargetUnit_SameAsSecondOperand()
        {
            var q1 = new QuantityLength(2.0, LengthUnit.Yard);
            var q2 = new QuantityLength(3.0, LengthUnit.Feet);
            var result = QuantityLength.Add(q1, q2, LengthUnit.Feet);
            Assert.AreEqual(9.0, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [Test]
        public void Addition_ExplicitTargetUnit_Commutativity()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            var result1 = QuantityLength.Add(q1, q2, LengthUnit.Yard);
            var result2 = QuantityLength.Add(q2, q1, LengthUnit.Yard);
            Assert.AreEqual(result1.Value, result2.Value, 1e-6);
            Assert.AreEqual(result1.Unit, result2.Unit);
        }

        [Test]
        public void Addition_ExplicitTargetUnit_WithZero()
        {
            var q1 = new QuantityLength(5.0, LengthUnit.Feet);
            var q2 = new QuantityLength(0.0, LengthUnit.Inch);
            var result = QuantityLength.Add(q1, q2, LengthUnit.Yard);
            Assert.AreEqual(1.667, result.Value, 1e-3);
            Assert.AreEqual(LengthUnit.Yard, result.Unit);
        }

        [Test]
        public void Addition_ExplicitTargetUnit_NegativeValues()
        {
            var q1 = new QuantityLength(5.0, LengthUnit.Feet);
            var q2 = new QuantityLength(-2.0, LengthUnit.Feet);
            var result = QuantityLength.Add(q1, q2, LengthUnit.Inch);
            Assert.AreEqual(36.0, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Inch, result.Unit);
        }

        [Test]
        public void Addition_ExplicitTargetUnit_NullTargetUnit_Throws()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            Assert.Throws<System.ArgumentException>(() => QuantityLength.Add(q1, q2, (LengthUnit)999));
        }

        [Test]
        public void Addition_ExplicitTargetUnit_LargeToSmallScale()
        {
            var q1 = new QuantityLength(1000.0, LengthUnit.Feet);
            var q2 = new QuantityLength(500.0, LengthUnit.Feet);
            var result = QuantityLength.Add(q1, q2, LengthUnit.Inch);
            Assert.AreEqual(18000.0, result.Value, 1e-2);
            Assert.AreEqual(LengthUnit.Inch, result.Unit);
        }

        [Test]
        public void Addition_ExplicitTargetUnit_SmallToLargeScale()
        {
            var q1 = new QuantityLength(12.0, LengthUnit.Inch);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            var result = QuantityLength.Add(q1, q2, LengthUnit.Yard);
            Assert.AreEqual(0.667, result.Value, 1e-3);
            Assert.AreEqual(LengthUnit.Yard, result.Unit);
        }
    }
}
