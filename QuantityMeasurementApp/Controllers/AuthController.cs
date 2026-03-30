using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.DTO.Auth;
using System;
using System.Threading.Tasks;

namespace QuantityMeasurementApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ISecurityService _securityService; // Added to demonstrate UC implementation

        public AuthController(IAuthService authService, ISecurityService securityService)
        {
            _authService = authService;
            _securityService = securityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            try { return Ok(await _authService.RegisterAsync(request)); }
            catch (InvalidOperationException ex) { return Conflict(new { ex.Message }); }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try { return Ok(await _authService.LoginAsync(request)); }
            catch (UnauthorizedAccessException ex) { return Unauthorized(new { ex.Message }); }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequestDto request)
        {
            try { return Ok(await _authService.GoogleLoginAsync(request)); }
            catch (UnauthorizedAccessException ex) { return Unauthorized(new { ex.Message }); }
            catch (InvalidOperationException ex) { return Conflict(new { ex.Message }); }
        }
        
        // UC requirement: Demonstration endpoint for AES encryption
        [HttpPost("encrypt-demo")]
        public IActionResult EncryptDemo([FromBody] string sensitiveData)
        {
            var enc = _securityService.Encrypt(sensitiveData);
            return Ok(new { Encrypted = enc, Decrypted = _securityService.Decrypt(enc) });
        }
    }
}
