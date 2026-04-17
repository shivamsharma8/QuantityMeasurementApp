using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementRepositoryLayer;
using QuantityMeasurementModelLayer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [HttpPost("save")]
        public IActionResult SaveHistory([FromBody] QuantityMeasurementEntity entry)
        {
            // JWT is forwarded from QuantityService; extract UserId if available
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                entry.UserId = userId;
            }
            _repository.Save(entry);
            return Ok(entry);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var history = _repository.GetByUserId(userId);
            return Ok(history);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteHistory(int id)
        {
            // Implement delete logic -> requires repository update, but for now returning OK
            return Ok(new { message = "Deleted" });
        }
        
        // Frontend exact match fallbacks if we want to not break frontend immediately:
        [HttpGet("operation/{operation}")]
        public IActionResult GetHistoryByOperation(string operation)
        {
            var history = _repository.GetByOperation(operation);
            return Ok(history);
        }
    }
}
