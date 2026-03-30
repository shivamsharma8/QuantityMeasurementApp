using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.DTO.Auth;
using QuantityMeasurementRepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantityMeasurementBusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo) 
        { 
            _repo = repo; 
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _repo.GetAllAsync();
            return users.Select(u => new UserResponseDto {
                Id = u.Id, Email = u.Email, FullName = u.FullName, 
                Role = u.Role, IsActive = u.IsActive, Provider = u.Provider
            });
        }

        public async Task ToggleUserActiveStatusAsync(Guid id, bool isActive)
        {
            var user = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("User not found");
            user.IsActive = isActive;
            user.UpdatedAt = DateTime.UtcNow;
            await _repo.UpdateAsync(user);
        }

        public async Task UpdateRoleAsync(Guid id, string role)
        {
            var user = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("User not found");
            user.Role = role;
            user.UpdatedAt = DateTime.UtcNow;
            await _repo.UpdateAsync(user);
        }
    }
}
