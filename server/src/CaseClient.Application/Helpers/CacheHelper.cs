using Microsoft.Extensions.Caching.Distributed;

using Microsoft.Extensions.Caching.Memory;

public static class CacheHelper
{
    public static async Task<T?> GetOrSetCacheAsync<T>(
        IMemoryCache cache,
        string key,
        Func<Task<T>> fetchData,
        TimeSpan? expirationTime = null) where T : class
    {
        if (cache.TryGetValue(key, out T cachedData))
        {
            return cachedData;
        }

        var data = await fetchData();
        if (data != null)
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime ?? TimeSpan.FromSeconds(5)
            };

            cache.Set(key, data, cacheOptions);
        }

        return data;
    }
}
