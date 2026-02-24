using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class QuantityMeasurementService
    {
        public bool AreEqual(QuantityLength? first, QuantityLength? second)
        {
            if (first == null || second == null)
                return false;
            return first.Equals(second);
        }
    }
}