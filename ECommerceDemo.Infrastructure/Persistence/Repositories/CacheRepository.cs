using ECommerceDemo.Application.Abstractions.Persistence;
using StackExchange.Redis;
using System.Text.Json;

namespace ECommerceDemo.Infrastructure.Persistence.Repositories;

public class CacheRepository : ICacheRepository
{
    private readonly IConnectionMultiplexer _redis;

    public CacheRepository(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var db = _redis.GetDatabase();
        var value = await db.StringGetAsync(key);
        if (!value.HasValue) return default;

        return JsonSerializer.Deserialize<T>(value);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan ttl)
    {
        var db = _redis.GetDatabase();
        await db.StringSetAsync(key, JsonSerializer.Serialize(value), ttl);
    }

    public async Task RemoveAsync(string key)
    {
        var db = _redis.GetDatabase();
        await db.KeyDeleteAsync(key);
    }
}
