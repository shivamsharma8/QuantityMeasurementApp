using System.Collections.Generic;
using QuantityMeasurementModelLayer;

namespace QuantityMeasurementRepositoryLayer
{
    public interface IQuantityMeasurementRepository
    {
        void Save(QuantityMeasurementEntity entity);

        QuantityMeasurementEntity? GetLastMeasurement();

        List<QuantityMeasurementEntity> GetAllMeasurements();

        List<QuantityMeasurementEntity> GetByUserId(string userId);
        
        List<QuantityMeasurementEntity> GetByOperation(string operation);
        
        List<QuantityMeasurementEntity> GetByCategory(string category);
        
        int GetOperationCount(string operation);
    }
}