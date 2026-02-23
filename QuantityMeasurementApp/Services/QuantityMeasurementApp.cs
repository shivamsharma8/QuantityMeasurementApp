using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class QuantityMeasurementService
    {
        public bool AreEqual(Feet? first, Feet? second)
        {
            if (first == null || second == null)
                return false;
            return first.Equals(second);
        }

        public bool AreEqual(Inches? first, Inches? second)
        {
            if (first == null || second == null)
                return false;
            return first.Equals(second);
        }
    }
}