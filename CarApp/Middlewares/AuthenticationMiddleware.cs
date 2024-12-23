using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using CarApp.Configs;
using CarApp.DTOs;
using CarApp.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;

namespace CarApp.Middlewares;

public class AuthenticationMiddleware(
    RequestDelegate next,
    IDistributedCache cache,
    BaseConfig config,
    IHttpContextAccessor httpContextAccessor)
{
    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        var metadata = context.GetEndpoint()?.Metadata;

        if (metadata is null)
        {
            await next(context);
            return;
        }

        if (token != null && metadata.Any(x => x is AuthorizeAttribute))
            await CheckUser(context, token, default);

        await next(context);
    }

    private async Task CheckUser(HttpContext context, string token, CancellationToken cancellationToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Auth.SecretKey).ToArray()),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out var validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;

        var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

        if (userId is null)
            throw new UnauthorizedAccessException("User is not valid", new UserNotValidException());

        context.Session.Set("userid", Encoding.UTF8.GetBytes(userId.Value));
        
        var sessionString = await cache.GetStringAsync($"userSession-{userId.Value}", cancellationToken);

        if (string.IsNullOrEmpty(sessionString))
            throw new UnauthorizedAccessException("Session is not valid", new SessionNotValidException());

        var session = JsonSerializer.Deserialize<RedisSession>(sessionString);

        if (session == null || session.Id.ToString() != userId.Value)
            throw new UnauthorizedAccessException("Session is not valid", new SessionNotValidException());
    }
}