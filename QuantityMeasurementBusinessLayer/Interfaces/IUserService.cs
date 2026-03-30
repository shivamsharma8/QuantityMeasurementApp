using QuantityMeasurementModelLayer.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuantityMeasurementBusinessLayer.Interfaces
{
    public interface IUserService 
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task ToggleUserActiveStatusAsync(Guid id, bool isActive);
        Task UpdateRoleAsync(Guid id, string role);
    }
}
