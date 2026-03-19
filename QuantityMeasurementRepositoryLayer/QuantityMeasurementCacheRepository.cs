using System.Collections.Generic;
using QuantityMeasurementModelLayer;


namespace QuantityMeasurementRepositoryLayer
{
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        private readonly List<QuantityMeasurementEntity> _measurements;

        public QuantityMeasurementCacheRepository()
        {
            _measurements = new List<QuantityMeasurementEntity>();
        }

        public void Save(QuantityMeasurementEntity entity)
        {
            _measurements.Add(entity);
        }

        public QuantityMeasurementEntity? GetLastMeasurement()
        {
            if (_measurements.Count == 0)
            {
                return null;
            }

            return _measurements[_measurements.Count - 1];
        }

        public List<QuantityMeasurementEntity> GetAllMeasurements()
        {
            return new List<QuantityMeasurementEntity>(_measurements);
        }
    }
}