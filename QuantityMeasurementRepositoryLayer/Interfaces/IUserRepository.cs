using QuantityMeasurementModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuantityMeasurementRepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByGoogleIdAsync(string googleId);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
