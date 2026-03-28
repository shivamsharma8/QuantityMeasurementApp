using System.Collections.Generic;
using System.Linq;
using QuantityMeasurementModelLayer;
using QuantityMeasurementModelLayer.Enums;
using System;

namespace QuantityMeasurementRepositoryLayer
{
    public class QuantityMeasurementEfRepository : IQuantityMeasurementRepository
    {
        private readonly QuantityMeasurementDbContext _context;

        public QuantityMeasurementEfRepository(QuantityMeasurementDbContext context)
        {
            _context = context;
        }

        public void Save(QuantityMeasurementEntity entity)
        {
            _context.Measurements.Add(entity);
            _context.SaveChanges();
        }

        public QuantityMeasurementEntity? GetLastMeasurement()
        {
            return _context.Measurements.OrderByDescending(m => m.CreatedAt).FirstOrDefault();
        }

        public List<QuantityMeasurementEntity> GetAllMeasurements()
        {
            return _context.Measurements.OrderByDescending(m => m.CreatedAt).ToList();
        }

        public List<QuantityMeasurementEntity> GetByOperation(string operation)
        {
            return _context.Measurements
                .Where(m => m.OperationType.ToLower() == operation.ToLower())
                .OrderByDescending(m => m.CreatedAt)
                .ToList();
        }

        public List<QuantityMeasurementEntity> GetByCategory(string category)
        {
            return _context.Measurements
                .Where(m => m.Category.ToLower() == category.ToLower())
                .OrderByDescending(m => m.CreatedAt)
                .ToList();
        }

        public int GetOperationCount(string operation)
        {
            return _context.Measurements
                .Count(m => m.OperationType.ToLower() == operation.ToLower());
        }
    }
}
