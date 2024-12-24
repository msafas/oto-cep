using CarApp.DTOs;
using CarApp.DTOs.User;
using CarApp.Entities;
using CarApp.ValueObjects;

namespace CarApp.Extensions;

public static class BaseExtensions
{
    public static T SetBaseProperties<T>(this T entity, RedisSession user) where T : BaseEntity
    {
        entity.CreatedById = user.Id;
        entity.CreatedBy = new UserValueObject
        {
            Id = user.Id,
        };
        entity.CreatedAt = DateTime.UtcNow;

        return entity;
    }

    public static T SetBaseUpdateProperties<T>(this T entity, RedisSession user) where T : BaseEntity
    {
        entity.UpdatedById = user.Id;
        entity.UpdatedBy = new UserValueObject
        {
            Id = user.Id,
        };
        entity.UpdatedAt = DateTime.UtcNow;

        return entity;
    }
}