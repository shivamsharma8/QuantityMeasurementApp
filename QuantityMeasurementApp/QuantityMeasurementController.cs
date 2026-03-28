using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementRepositoryLayer;

namespace QuantityMeasurementApp.Controllers
{
    [ApiController]
    [Route("api/v1/quantities")]
    public class QuantityMeasurementController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;
        private readonly IQuantityMeasurementRepository _repository;

        public QuantityMeasurementController(IQuantityMeasurementService service, IQuantityMeasurementRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        [HttpPost("compare")]
        public IActionResult Compare([FromBody] QuantityInputDto request)
        {
            var result = _service.ProcessCompare(request);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("convert")]
        public IActionResult Convert([FromBody] QuantityInputDto request)
        {
            var result = _service.ProcessConvert(request);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] QuantityInputDto request)
        {
            var result = _service.ProcessAdd(request);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("subtract")]
        public IActionResult Subtract([FromBody] QuantityInputDto request)
        {
            var result = _service.ProcessSubtract(request);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("divide")]
        public IActionResult Divide([FromBody] QuantityInputDto request)
        {
            var result = _service.ProcessDivide(request);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("history/operation/{operation}")]
        public IActionResult GetHistoryByOperation(string operation)
        {
            var history = _repository.GetByOperation(operation);
            return Ok(history);
        }

        [HttpGet("history/type/{category}")]
        public IActionResult GetHistoryByCategory(string category)
        {
            var history = _repository.GetByCategory(category);
            return Ok(history);
        }

        [HttpGet("count/{operation}")]
        public IActionResult GetOperationCount(string operation)
        {
            var count = _repository.GetOperationCount(operation);
            return Ok(new { Operation = operation, Count = count });
        }

        [HttpGet("categories/units")]
        public IActionResult GetAllCategoriesAndUnits()
        {
            var result = new
            {
                Length = System.Enum.GetNames(typeof(QuantityMeasurementModelLayer.LengthUnit)),
                Weight = System.Enum.GetNames(typeof(QuantityMeasurementModelLayer.WeightUnit)),
                Volume = System.Enum.GetNames(typeof(QuantityMeasurementModelLayer.VolumeUnit)),
                Temperature = System.Enum.GetNames(typeof(QuantityMeasurementModelLayer.TemperatureUnit)),
                Categories = new[] { "length", "weight", "volume", "temperature" }
            };
            return Ok(result);
        }

        [HttpGet("categories/{category}/units")]
        public IActionResult GetUnitsByCategory(string category)
        {
            switch (category.ToLower())
            {
                case "length":
                    return Ok(System.Enum.GetNames(typeof(QuantityMeasurementModelLayer.LengthUnit)));
                case "weight":
                    return Ok(System.Enum.GetNames(typeof(QuantityMeasurementModelLayer.WeightUnit)));
                case "volume":
                    return Ok(System.Enum.GetNames(typeof(QuantityMeasurementModelLayer.VolumeUnit)));
                case "temperature":
                    return Ok(System.Enum.GetNames(typeof(QuantityMeasurementModelLayer.TemperatureUnit)));
                default:
                    return BadRequest(new ErrorResponseDto { Message = "Unsupported category type.", StatusCode = 400 });
            }
        }
    }
}