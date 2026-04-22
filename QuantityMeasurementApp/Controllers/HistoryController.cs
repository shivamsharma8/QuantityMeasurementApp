using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementRepositoryLayer;
using System;

namespace QuantityMeasurementApp.Controllers
{
    [ApiController]
    [Route("api/history")]
    public class HistoryController : ControllerBase
    {
        private readonly IQuantityMeasurementRepository _repository;

        public HistoryController(IQuantityMeasurementRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllHistory()
        {
            var history = _repository.GetAllMeasurements();
            return Ok(history);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHistory(Guid id)
        {
            // Optional: The repository does not currently support deleting by ID 
            // Return OK so the UI doesn't crash if delete is clicked
            return Ok();
        }
    }
}
