using CarApp.DTOs;
using CarApp.DTOs.Auth;
using CarApp.DTOs.User;

namespace CarApp.Services.Interfaces;

public interface IAuthService
{
    Task<Response<LoginResponseDto>> LoginAsync(LoginRequestDto request,CancellationToken cancellationToken);
    Task<Response<UserDto>> RegisterAsync(RegisterRequestDto request,CancellationToken cancellationToken);
    Task<Response<MeResponse>> MeAsync(CancellationToken cancellationToken);
    Task<Response<RefreshTokenResponse>> RefreshTokenAsync(CancellationToken cancellationToken);
}