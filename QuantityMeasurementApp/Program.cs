using QuantityMeasurementBusinessLayer;

namespace QuantityMeasurementApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var service = new QuantityMeasurementService();
            var controller = new QuantityMeasurementController(service);

            QuantityMeasurementMenu.ShowMenu(controller);
        }
    }
}