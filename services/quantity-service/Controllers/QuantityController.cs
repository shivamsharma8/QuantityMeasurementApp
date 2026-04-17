using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace QuantityMeasurementApp.Controllers
{
    [ApiController]
    [Route("api/quantity")]
    public class QuantityController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;
        private readonly IHttpClientFactory _httpClientFactory;

        public QuantityController(IQuantityMeasurementService service, IHttpClientFactory httpClientFactory)
        {
            _service = service;
            _httpClientFactory = httpClientFactory;
        }
        
        private static string? ExtractUserIdFromJwt(string? bearerHeader)
        {
            if (string.IsNullOrEmpty(bearerHeader) || !bearerHeader.StartsWith("Bearer "))
                return null;
            var token = bearerHeader.Substring("Bearer ".Length).Trim();
            try
            {
                // JWT is header.payload.signature — decode payload
                var parts = token.Split('.');
                if (parts.Length != 3) return null;
                var payload = parts[1];
                // Base64URL decode
                var padded = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
                var json = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(padded.Replace('-', '+').Replace('_', '/')));
                using var doc = System.Text.Json.JsonDocument.Parse(json);
                // Try "sub" first (standard), then "nameid" (ASP.NET Core default)
                if (doc.RootElement.TryGetProperty("sub", out var sub))
                    return sub.GetString();
                if (doc.RootElement.TryGetProperty("nameid", out var nameid))
                    return nameid.GetString();
                if (doc.RootElement.TryGetProperty("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", out var ni))
                    return ni.GetString();
            }
            catch { /* ignore parse errors */ }
            return null;
        }

        private async Task SendHistoryAsync(QuantityInputDto input, QuantityMeasurementDto result, string operation, HttpContext context)
        {
            context.Request.Headers.TryGetValue("Authorization", out var authHeader);
            var userId = ExtractUserIdFromJwt(authHeader.ToString());

            var entity = new QuantityMeasurementEntity
            {
                UserId = userId ?? string.Empty,
                OperationType = operation,
                IsSuccess = result.IsSuccess,
                Category = input.Category,
                Value1 = input.Value1,
                Unit1 = input.Unit1,
                Value2 = input.Value2 ?? 0,
                Unit2 = input.Unit2 ?? string.Empty,
                TargetUnit = input.TargetUnit ?? string.Empty,
                AdditionResult = operation == "Add" ? result.Result : string.Empty,
                SubtractionResult = operation == "Subtract" ? result.Result : string.Empty,
                DivisionResult = operation == "Divide" ? result.DivisionResult : 0,
                IsEqual = operation == "Compare" && result.IsEqual,
                CreatedAt = DateTime.UtcNow
            };

            var client = _httpClientFactory.CreateClient();
            if (!string.IsNullOrEmpty(authHeader.ToString()))
            {
                client.DefaultRequestHeaders.Add("Authorization", authHeader.ToString());
            }

            try
            {
                await client.PostAsJsonAsync("http://history-service:5003/api/history/save", entity);
            }
            catch { /* Suppress history saving error to prevent calculation failure */ }
        }

        [HttpPost("compare")]
        public async Task<IActionResult> Compare([FromBody] QuantityInputDto request)
        {
            var result = _service.ProcessCompare(request);
            if (!result.IsSuccess) return BadRequest(result);
            
            await SendHistoryAsync(request, result, "Compare", HttpContext);
            
            return Ok(result);
        }

        [HttpPost("convert")]
        public async Task<IActionResult> Convert([FromBody] QuantityInputDto request)
        {
            var result = _service.ProcessConvert(request);
            if (!result.IsSuccess) return BadRequest(result);

            await SendHistoryAsync(request, result, "Convert", HttpContext);

            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] QuantityInputDto request)
        {
            var result = _service.ProcessAdd(request);
            if (!result.IsSuccess) return BadRequest(result);

            await SendHistoryAsync(request, result, "Add", HttpContext);

            return Ok(result);
        }

        [HttpPost("subtract")]
        public async Task<IActionResult> Subtract([FromBody] QuantityInputDto request)
        {
            var result = _service.ProcessSubtract(request);
            if (!result.IsSuccess) return BadRequest(result);

            await SendHistoryAsync(request, result, "Subtract", HttpContext);

            return Ok(result);
        }

        [HttpPost("divide")]
        public async Task<IActionResult> Divide([FromBody] QuantityInputDto request)
        {
            var result = _service.ProcessDivide(request);
            if (!result.IsSuccess) return BadRequest(result);

            await SendHistoryAsync(request, result, "Divide", HttpContext);

            return Ok(result);
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
