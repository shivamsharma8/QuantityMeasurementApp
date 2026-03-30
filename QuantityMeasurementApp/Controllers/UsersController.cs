using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementBusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuantityMeasurementApp.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = "Admin")] // ONLY ADMIN CAN ACCESS THIS ENTIRE CONTROLLER
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _userService.GetAllUsersAsync());

        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] string role)
        {
            try {
                await _userService.UpdateRoleAsync(id, role);
                return Ok(new { message = "Role updated." });
            } catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> ToggleStatus(Guid id, [FromQuery] bool isActive)
        {
            try {
                await _userService.ToggleUserActiveStatusAsync(id, isActive);
                return Ok(new { message = $"User {(isActive ? "activated" : "deactivated")}." });
            } catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }
    }
}
