using Microsoft.AspNetCore.Identity;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.Entities;
using System;
using System.Security.Cryptography;
using System.Text;

namespace QuantityMeasurementBusinessLayer.Services
{
    public class HashingService : IHashingService
    {
        // Using ASP.NET Core Identity's secure PasswordHasher (PBKDF2 under the hood by default)
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public string HashPassword(string password)
        {
            // Dummy user instance needed for PasswordHasher API
            return _passwordHasher.HashPassword(new User(), password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            var result = _passwordHasher.VerifyHashedPassword(new User(), hash, password);
            return result == PasswordVerificationResult.Success;
        }

        // General purpose SHA256 (Used for data integrity verification, NEVER for passwords)
        public string ComputeSha256(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
