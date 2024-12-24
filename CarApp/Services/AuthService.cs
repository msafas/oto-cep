using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using AutoMapper;
using CarApp.DTOs;
using CarApp.DTOs.Auth;
using CarApp.DTOs.User;
using CarApp.Entities;
using CarApp.Services.Interfaces;
using CarApp.ValueObjects;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CarApp.Services;

public class AuthService(
    IMongoDatabase database,
    IMapper mapper,
    ISharedIdentityService identityService,
    IDistributedCache cache) : IAuthService
{
    private readonly IMongoCollection<User> _users = database.GetCollection<User>("User");
    private const int RefreshTokenExpiration = 30;
    private const string SecretKey = "car-app-secret-key";
    private const string ValidIssuer = "car-app";
    private const string ValidAudience = "car-app";

    public async Task<Response<LoginResponseDto>> LoginAsync(LoginRequestDto request,
        CancellationToken cancellationToken)
    {
        var user = await _users.AsQueryable().FirstOrDefaultAsync(x => x.Phone == request.Phone, cancellationToken);

        if (user == null)
        {
            return Response<LoginResponseDto>.Fail("User not found");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return Response<LoginResponseDto>.Fail("Password is incorrect");
        }

        var userDto = mapper.Map<UserDto>(user);


        return Response<LoginResponseDto>.Success(await DoLoginAndTokenJobs(userDto));
    }

    public async Task<Response<UserDto>> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken)
    {
        var userExist = await _users.AsQueryable().Where(x => x.Phone == request.Phone || x.Email == request.Email)
            .AnyAsync(cancellationToken);

        if (userExist)
        {
            return Response<UserDto>.Fail("User with this email or phone number already exists");
        }

        var user = mapper.Map<User>(request);

        user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

        await _users.InsertOneAsync(user, cancellationToken: cancellationToken);

        return Response<UserDto>.Success(mapper.Map<UserDto>(user));
    }

    public async Task<Response<MeResponse>> MeAsync(CancellationToken cancellationToken)
    {
        var session = identityService.GetSession;

        var userDto = mapper.Map<UserDto>(session);

        return Response<MeResponse>.Success(new MeResponse { User = userDto });
    }

    public async Task<Response<RefreshTokenResponse>> RefreshTokenAsync(CancellationToken cancellationToken)
    {
        var session = identityService.GetSession;

        var user = await _users.AsQueryable().FirstOrDefaultAsync(x => x.Id == session.Id, cancellationToken);

        if (user == null)
        {
            return Response<RefreshTokenResponse>.Fail("User not found");
        }

        var token = await DoLoginAndTokenJobs(mapper.Map<UserDto>(user));

        var response = new RefreshTokenResponse
        {
            Token = token.Token,
            Expiration = token.Expiration
        };

        return Response<RefreshTokenResponse>.Success(response);
    }


    private async Task<LoginResponseDto> DoLoginAndTokenJobs(UserDto user)
    {
        var session = mapper.Map<RedisSession>(user);

        await cache.SetStringAsync("userSession-" + user.Id,
            JsonSerializer.Serialize(session), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(120)
            });


        var authClaim = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new("userid", user.Id.ToString()),
        };


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(authClaim),
            Expires = DateTime.Now.AddHours(RefreshTokenExpiration),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey).ToArray()),
                SecurityAlgorithms.HmacSha256Signature),
            Audience = ValidAudience,
            Issuer = ValidIssuer
        };

        var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);

        var response = new LoginResponseDto
        {
            User = user,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo
        };
        return response;
    }
}