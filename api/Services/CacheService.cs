using System.Collections.Concurrent;
using System.Text.Json;
using api.Services.interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace api.Services;

public class CacheService :ICacheService

{
    private static ConcurrentDictionary<string, bool> CacheKeys = new();
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
    where T :class
    {
        string? cacheValue = await _distributedCache.GetStringAsync(key, cancellationToken);
        if (cacheValue is null)
        {
            return null;
        }

        T? value = JsonSerializer.Deserialize<T>(cacheValue);
        return value;
    }

    public async Task SetAsync<T>(string key,T value, CancellationToken cancellationToken = default)where T:class
    {
        string cachedValue = JsonSerializer.Serialize(value);
        await _distributedCache.SetStringAsync(key, cachedValue, cancellationToken);
        CacheKeys.TryAdd(key, false);

    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _distributedCache.RemoveAsync(key, cancellationToken);
        CacheKeys.Remove(key,out bool _);
    }

    public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
    {
        IEnumerable<Task> tasks = CacheKeys
            .Keys
            .Where(k => k.StartsWith(prefixKey))
            .Select(k => RemoveAsync(k, cancellationToken));

        await Task.WhenAll(tasks);
    }
}
