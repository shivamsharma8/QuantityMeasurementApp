using QuantityMeasurementModelLayer.DTO.Auth;
using System.Threading.Tasks;

namespace QuantityMeasurementBusinessLayer.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
        Task<AuthResponseDto> GoogleLoginAsync(GoogleLoginRequestDto request);
    }
}
