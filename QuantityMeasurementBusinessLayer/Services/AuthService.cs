using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.DTO.Auth;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuantityMeasurementBusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashingService _hashingService;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, IHashingService hashingService, IConfiguration config)
        {
            _userRepository = userRepository;
            _hashingService = hashingService;
            _config = config;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                throw new InvalidOperationException("Email already in use.");

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = _hashingService.HashPassword(request.Password),
                Role = "User",
                Provider = "Local"
            };

            await _userRepository.AddAsync(user);

            return new AuthResponseDto
            {
                Token = GenerateJwtToken(user),
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || user.Provider != "Local" || !user.IsActive)
                throw new UnauthorizedAccessException("Invalid credentials or deactivated account.");

            if (!_hashingService.VerifyPassword(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials.");

            return new AuthResponseDto
            {
                Token = GenerateJwtToken(user),
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role
            };
        }

        public async Task<AuthResponseDto> GoogleLoginAsync(GoogleLoginRequestDto request)
        {
            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _config["GoogleAuth:ClientId"] }
                });
            }
            catch (InvalidJwtException)
            {
                throw new UnauthorizedAccessException("Invalid Google Token.");
            }

            var user = await _userRepository.GetByGoogleIdAsync(payload.Subject);
            
            if (user == null)
            {
                var existingEmail = await _userRepository.GetByEmailAsync(payload.Email);
                if (existingEmail != null)
                    throw new InvalidOperationException("Email already associated with a local account. Login with password.");

                user = new User
                {
                    Email = payload.Email,
                    FullName = payload.Name,
                    GoogleId = payload.Subject,
                    Provider = "Google",
                    IsActive = true,
                    Role = "User",
                    PasswordHash = "Google_SSO_Has_No_Password" 
                };
                await _userRepository.AddAsync(user);
            }
            else if (!user.IsActive)
            {
                throw new UnauthorizedAccessException("Account is deactivated.");
            }

            return new AuthResponseDto
            {
                Token = GenerateJwtToken(user),
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role
            };
        }

        private string GenerateJwtToken(User user)
        {
            var jwtConfig = _config.GetSection("Jwt");
            var keyStr = jwtConfig["Key"];
            if (string.IsNullOrEmpty(keyStr)) throw new ArgumentNullException("JWT Key is missing from configuration.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("Provider", user.Provider)
            };

            var token = new JwtSecurityToken(
                issuer: jwtConfig["Issuer"],
                audience: jwtConfig["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtConfig["DurationInMinutes"] ?? "60")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
