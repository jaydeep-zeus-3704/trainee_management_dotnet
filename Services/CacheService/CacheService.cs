using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

public interface ICacheService
{
     public  Task SetDataAsync <T>(string key, T data);
     public  Task<T?> GetDataAsync<T>(string key);
     public  Task DeleteAsync(string key);
}


public class CacheService:ICacheService {
    private readonly IDistributedCache _cache; 
    
    public CacheService(IDistributedCache cache) {
        _cache = cache;
    }

    public async Task SetDataAsync <T>(string key, T data)
    {
         DistributedCacheEntryOptions options = new DistributedCacheEntryOptions {
               AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
               SlidingExpiration = TimeSpan.FromMinutes(5)
            };

        string serializedData=JsonSerializer.Serialize(data);
        await _cache.SetStringAsync(key,serializedData,options);
     
    }

    public async Task<T?> GetDataAsync<T>(string key) {
        string? cachedData = await _cache.GetStringAsync(key);
        if (string.IsNullOrEmpty(cachedData))
        {
            return default;
        }
        T data=JsonSerializer.Deserialize<T>(cachedData)!;
        return data;
    }

    public async Task DeleteAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}