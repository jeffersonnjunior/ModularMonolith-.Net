namespace Common.ICache.Services;

public interface ICacheService
{
    void Set<T>(string key, T value, TimeSpan? expiry = null);
    T? Get<T>(string key);
    void Remove(string key);
    bool Exists(string key);
    void FlushDatabase();
}
