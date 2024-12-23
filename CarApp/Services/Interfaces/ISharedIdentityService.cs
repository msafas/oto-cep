using CarApp.DTOs;

namespace CarApp.Services.Interfaces;

public interface ISharedIdentityService
{
    public string GetUserId { get; }
    public RedisSession GetSession { get; }
}