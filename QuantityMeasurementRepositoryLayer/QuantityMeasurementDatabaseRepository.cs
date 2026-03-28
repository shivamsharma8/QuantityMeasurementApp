using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using QuantityMeasurementModelLayer;
using QuantityMeasurementRepositoryLayer.Exceptions;

namespace QuantityMeasurementRepositoryLayer
{
    public class QuantityMeasurementDatabaseRepository : IQuantityMeasurementRepository
    {
        private readonly string _connectionString;

        public QuantityMeasurementDatabaseRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string cannot be null or empty.", nameof(connectionString));
            }
            _connectionString = connectionString;
        }

        public void Save(QuantityMeasurementEntity entity)
        {
            // Mapping existing entity properties to the user's created SQL table columns
            string resultText = $"Add: {entity.AdditionResult}, Sub: {entity.SubtractionResult}, Eq: {entity.IsEqual}";
            
            const string query = @"
                INSERT INTO QuantityMeasurements 
                (MeasurementType, OperationType, Unit1, Value1, Unit2, Value2, ResultValue, ResultText)
                VALUES 
                (@MeasurementType, @OperationType, @Unit1, @Value1, @Unit2, @Value2, @ResultValue, @ResultText)";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MeasurementType", string.IsNullOrWhiteSpace(entity.Category) ? "Unknown" : entity.Category);
                    command.Parameters.AddWithValue("@OperationType", "AllOperations");
                    command.Parameters.AddWithValue("@Unit1", string.IsNullOrWhiteSpace(entity.Unit1) ? (object)DBNull.Value : entity.Unit1);
                    command.Parameters.AddWithValue("@Value1", entity.Value1);
                    command.Parameters.AddWithValue("@Unit2", string.IsNullOrWhiteSpace(entity.Unit2) ? (object)DBNull.Value : entity.Unit2);
                    command.Parameters.AddWithValue("@Value2", entity.Value2);
                    command.Parameters.AddWithValue("@ResultValue", entity.DivisionResult);
                    command.Parameters.AddWithValue("@ResultText", resultText);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException($"Database save error: {ex.Message}", ex);
            }
        }

        public QuantityMeasurementEntity? GetLastMeasurement()
        {
            const string query = @"
                SELECT TOP 1 Id, MeasurementType, OperationType, Unit1, Value1, Unit2, Value2, ResultValue, ResultText, CreatedAt
                FROM QuantityMeasurements
                ORDER BY CreatedAt DESC";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToEntity(reader);
                        }
                    }
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("An error occurred while retrieving the last measurement from the database.", ex);
            }
        }

        public List<QuantityMeasurementEntity> GetAllMeasurements()
        {
            var measurements = new List<QuantityMeasurementEntity>();
            const string query = @"
                SELECT Id, MeasurementType, OperationType, Unit1, Value1, Unit2, Value2, ResultValue, ResultText, CreatedAt
                FROM QuantityMeasurements
                ORDER BY CreatedAt DESC";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            measurements.Add(MapReaderToEntity(reader));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("An error occurred while retrieving all measurements from the database.", ex);
            }

            return measurements;
        }

        private QuantityMeasurementEntity MapReaderToEntity(SqlDataReader reader)
        {
            // Reverse map the SQL table back to the Entity
            var entity = new QuantityMeasurementEntity
            {
                // We generate a new Guid since the DB uses INT
                Id = Guid.NewGuid(),
                Category = reader.IsDBNull(reader.GetOrdinal("MeasurementType")) ? string.Empty : reader.GetString(reader.GetOrdinal("MeasurementType")),
                Value1 = reader.IsDBNull(reader.GetOrdinal("Value1")) ? 0 : reader.GetDouble(reader.GetOrdinal("Value1")),
                Unit1 = reader.IsDBNull(reader.GetOrdinal("Unit1")) ? string.Empty : reader.GetString(reader.GetOrdinal("Unit1")),
                Value2 = reader.IsDBNull(reader.GetOrdinal("Value2")) ? 0 : reader.GetDouble(reader.GetOrdinal("Value2")),
                Unit2 = reader.IsDBNull(reader.GetOrdinal("Unit2")) ? string.Empty : reader.GetString(reader.GetOrdinal("Unit2")),
                
                // We map ResultValue back to DivisionResult as that's where we put it
                DivisionResult = reader.IsDBNull(reader.GetOrdinal("ResultValue")) ? 0 : reader.GetDouble(reader.GetOrdinal("ResultValue")),
                CreatedAt = reader.IsDBNull(reader.GetOrdinal("CreatedAt")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
            };
            
            // Extract the string representations from ResultText
            if (!reader.IsDBNull(reader.GetOrdinal("ResultText")))
            {
                string resultText = reader.GetString(reader.GetOrdinal("ResultText"));
                entity.AdditionResult = resultText;
            }

            return entity;
        }

        public List<QuantityMeasurementEntity> GetByOperation(string operation)
        {
            throw new NotImplementedException("Not implemented for ADO.NET legacy repository.");
        }

        public List<QuantityMeasurementEntity> GetByCategory(string category)
        {
            throw new NotImplementedException("Not implemented for ADO.NET legacy repository.");
        }

        public int GetOperationCount(string operation)
        {
            throw new NotImplementedException("Not implemented for ADO.NET legacy repository.");
        }
    }
}
