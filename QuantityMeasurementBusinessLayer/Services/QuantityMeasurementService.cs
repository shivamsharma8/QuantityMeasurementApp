using System;

namespace QuantityMeasurementBusinessLayer
{
    /// <summary>
    /// Service layer for quantity measurement operations.
    /// Acts as a facade between presentation layer and domain logic.
    /// </summary>
    public class QuantityMeasurementService
    {
        /// <summary>
        /// Checks whether two quantities are equal after converting to base unit.
        /// </summary>
        public bool AreEqual<U>(Quantity<U> first, Quantity<U> second) where U : struct, Enum
        {
            if (first == null || second == null)
                throw new ArgumentException("Quantities must not be null.");

            return first.Equals(second);
        }

        /// <summary>
        /// Converts a quantity into the target unit.
        /// </summary>
        public Quantity<U> Convert<U>(Quantity<U> quantity, U targetUnit) where U : struct, Enum
        {
            if (quantity == null)
                throw new ArgumentException("Quantity must not be null.");

            return quantity.Convert(targetUnit);
        }

        /// <summary>
        /// Adds two quantities and returns result in first quantity's unit.
        /// </summary>
        public Quantity<U> Add<U>(Quantity<U> first, Quantity<U> second) where U : struct, Enum
        {
            if (first == null || second == null)
                throw new ArgumentException("Quantities must not be null.");

            return Quantity<U>.Add(first, second);
        }

        /// <summary>
        /// Adds two quantities and returns result in target unit.
        /// </summary>
        public Quantity<U> Add<U>(Quantity<U> first, Quantity<U> second, U targetUnit) where U : struct, Enum
        {
            if (first == null || second == null)
                throw new ArgumentException("Quantities must not be null.");

            return Quantity<U>.Add(first, second, targetUnit);
        }

        /// <summary>
        /// Subtracts second quantity from first and returns result in first quantity's unit.
        /// </summary>
        public Quantity<U> Subtract<U>(Quantity<U> first, Quantity<U> second) where U : struct, Enum
        {
            if (first == null || second == null)
                throw new ArgumentException("Quantities must not be null.");

            return Quantity<U>.Subtract(first, second);
        }

        /// <summary>
        /// Subtracts second quantity from first and returns result in target unit.
        /// </summary>
        public Quantity<U> Subtract<U>(Quantity<U> first, Quantity<U> second, U targetUnit) where U : struct, Enum
        {
            if (first == null || second == null)
                throw new ArgumentException("Quantities must not be null.");

            return Quantity<U>.Subtract(first, second, targetUnit);
        }

        /// <summary>
        /// Divides first quantity by second and returns ratio.
        /// </summary>
        public double Divide<U>(Quantity<U> first, Quantity<U> second) where U : struct, Enum
        {
            if (first == null || second == null)
                throw new ArgumentException("Quantities must not be null.");

            return first.Divide(second);
        }
    }
}