using System.Collections.Generic;
using QuantityMeasurementModelLayer;

namespace QuantityMeasurementRepositoryLayer
{
    public interface IQuantityMeasurementRepository
    {
        void Save(QuantityMeasurementEntity entity);

        QuantityMeasurementEntity? GetLastMeasurement();

        List<QuantityMeasurementEntity> GetAllMeasurements();
    }
}