using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;

namespace ShareServices.AsDatabase
{
    public class RedisDatabase : IRedisDatabase
    {
        private readonly IDistributedCache _cache;
        public RedisDatabase(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task SetRecordAsync<T>(string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
        {
            var opt = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromMinutes(60),
                SlidingExpiration = unusedExpireTime
            };
            var jsonData = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(recordId, jsonData, opt);
        }
        public async Task<T?> GetRecordAsync<T>(string recordId)
        {
            var data = await _cache.GetStringAsync(recordId);
            return data != null ? JsonSerializer.Deserialize<T>(data) : default;
        }


    }
}
