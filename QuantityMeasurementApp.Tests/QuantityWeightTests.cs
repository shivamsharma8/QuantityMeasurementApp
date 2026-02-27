using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityWeightTests
    {
        [Test]
        public void Equality_KilogramToKilogram_SameValue()
        {
            var q1 = new QuantityWeight(1.0, WeightUnit.Kilogram);
            var q2 = new QuantityWeight(1.0, WeightUnit.Kilogram);
            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void Equality_KilogramToKilogram_DifferentValue()
        {
            var q1 = new QuantityWeight(1.0, WeightUnit.Kilogram);
            var q2 = new QuantityWeight(2.0, WeightUnit.Kilogram);
            Assert.IsFalse(q1.Equals(q2));
        }

        [Test]
        public void Equality_KilogramToGram_EquivalentValue()
        {
            var q1 = new QuantityWeight(1.0, WeightUnit.Kilogram);
            var q2 = new QuantityWeight(1000.0, WeightUnit.Gram);
            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void Equality_GramToKilogram_EquivalentValue()
        {
            var q1 = new QuantityWeight(1000.0, WeightUnit.Gram);
            var q2 = new QuantityWeight(1.0, WeightUnit.Kilogram);
            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void Equality_PoundToKilogram_EquivalentValue()
        {
            var q1 = new QuantityWeight(2.20462, WeightUnit.Pound);
            var q2 = new QuantityWeight(1.0, WeightUnit.Kilogram);
            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void Conversion_KilogramToGram()
        {
            var q = new QuantityWeight(1.0, WeightUnit.Kilogram);
            var result = q.ConvertTo(WeightUnit.Gram);
            Assert.AreEqual(1000.0, result.Value, 1e-6);
            Assert.AreEqual(WeightUnit.Gram, result.Unit);
        }

        [Test]
        public void Conversion_GramToPound()
        {
            var q = new QuantityWeight(500.0, WeightUnit.Gram);
            var result = q.ConvertTo(WeightUnit.Pound);
            Assert.AreEqual(1.10231, result.Value, 1e-5);
            Assert.AreEqual(WeightUnit.Pound, result.Unit);
        }

        [Test]
        public void Addition_SameUnit_KilogramPlusKilogram()
        {
            var q1 = new QuantityWeight(1.0, WeightUnit.Kilogram);
            var q2 = new QuantityWeight(2.0, WeightUnit.Kilogram);
            var result = QuantityWeight.Add(q1, q2);
            Assert.AreEqual(3.0, result.Value, 1e-6);
            Assert.AreEqual(WeightUnit.Kilogram, result.Unit);
        }

        [Test]
        public void Addition_CrossUnit_KilogramPlusGram()
        {
            var q1 = new QuantityWeight(1.0, WeightUnit.Kilogram);
            var q2 = new QuantityWeight(1000.0, WeightUnit.Gram);
            var result = QuantityWeight.Add(q1, q2);
            Assert.AreEqual(2.0, result.Value, 1e-6);
            Assert.AreEqual(WeightUnit.Kilogram, result.Unit);
        }

        [Test]
        public void Addition_ExplicitTargetUnit_Gram()
        {
            var q1 = new QuantityWeight(1.0, WeightUnit.Kilogram);
            var q2 = new QuantityWeight(1000.0, WeightUnit.Gram);
            var result = QuantityWeight.Add(q1, q2, WeightUnit.Gram);
            Assert.AreEqual(2000.0, result.Value, 1e-6);
            Assert.AreEqual(WeightUnit.Gram, result.Unit);
        }

        [Test]
        public void Addition_Commutativity()
        {
            var q1 = new QuantityWeight(1.0, WeightUnit.Kilogram);
            var q2 = new QuantityWeight(1000.0, WeightUnit.Gram);
            var result1 = QuantityWeight.Add(q1, q2, WeightUnit.Kilogram);
            var result2 = QuantityWeight.Add(q2, q1, WeightUnit.Kilogram);
            Assert.AreEqual(result1.Value, result2.Value, 1e-6);
        }

        [Test]
        public void Addition_WithZero()
        {
            var q1 = new QuantityWeight(5.0, WeightUnit.Kilogram);
            var q2 = new QuantityWeight(0.0, WeightUnit.Gram);
            var result = QuantityWeight.Add(q1, q2);
            Assert.AreEqual(5.0, result.Value, 1e-6);
        }

        [Test]
        public void Addition_NegativeValues()
        {
            var q1 = new QuantityWeight(5.0, WeightUnit.Kilogram);
            var q2 = new QuantityWeight(-2000.0, WeightUnit.Gram);
            var result = QuantityWeight.Add(q1, q2);
            Assert.AreEqual(3.0, result.Value, 1e-6);
        }
    }
}
