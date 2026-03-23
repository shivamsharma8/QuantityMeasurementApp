using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using QuantityMeasurementBusinessLayer;
using QuantityMeasurementRepositoryLayer;

namespace QuantityMeasurementApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Set up configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Read repository type
            string repositoryType = configuration.GetSection("RepositorySettings")["RepositoryType"] ?? "Cache";
            
            IQuantityMeasurementRepository repository;

            if (repositoryType.Equals("Database", StringComparison.OrdinalIgnoreCase))
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    Console.WriteLine("Error: DefaultConnection string is missing in appsettings.json.");
                    return;
                }
                repository = new QuantityMeasurementDatabaseRepository(connectionString);
            }
            else
            {
                repository = new QuantityMeasurementCacheRepository();
            }

            // Inject the selected repository into the service layer
            var service = new QuantityMeasurementService(repository);
            var controller = new QuantityMeasurementController(service);

            QuantityMeasurementMenu.ShowMenu(controller);
        }
    }
}