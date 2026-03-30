using System.Collections.Generic;
using System.Linq;
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

        public List<QuantityMeasurementEntity> GetByOperation(string operation)
        {
            return _measurements.Where(m => m.OperationType.ToLower() == operation.ToLower()).OrderByDescending(m => m.CreatedAt).ToList();
        }

        public List<QuantityMeasurementEntity> GetByCategory(string category)
        {
            return _measurements.Where(m => m.Category.ToLower() == category.ToLower()).OrderByDescending(m => m.CreatedAt).ToList();
        }

        public int GetOperationCount(string operation)
        {
            return _measurements.Count(m => m.OperationType.ToLower() == operation.ToLower());
        }
    }
}