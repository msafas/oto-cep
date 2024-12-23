using System.Security.Claims;
using System.Text.Json;
using CarApp.DTOs;
using CarApp.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace CarApp.Services;

public class SharedIdentityService(IHttpContextAccessor _httpContextAccessor, IDistributedCache cache)
    : ISharedIdentityService
{
    public string GetUserId => _httpContextAccessor.HttpContext.Session.GetString("userid");

    public RedisSession GetSession =>
        JsonSerializer.Deserialize<RedisSession>(cache.GetString($"userSession-{GetUserId}"));
}