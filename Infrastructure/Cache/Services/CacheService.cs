using Common.ICache.Services;
using Infrastructure.Cache.Connection;
using StackExchange.Redis;

namespace Infrastructure.Cache.Services;

public sealed class CacheService : ICacheService
{
    private readonly IDatabase _database;

    public CacheService(RedisFactory redisFactory)
    {
        _database = redisFactory.GetDatabase();
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        await _database.StringSetAsync(
            key,
            System.Text.Json.JsonSerializer.Serialize(value),
            expiry
        );
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);
        return value.HasValue ?
            System.Text.Json.JsonSerializer.Deserialize<T>(value!) :
            default;
    }

    public async Task RemoveAsync(string key) => await _database.KeyDeleteAsync(key);

    public async Task<bool> ExistsAsync(string key) => await _database.KeyExistsAsync(key);

    public async Task FlushDatabaseAsync() => await _database.ExecuteAsync("FLUSHDB");
}