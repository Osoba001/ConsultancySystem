using LCS.Application.Messages;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;

namespace LCS.Message.Services
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _config;

        public RedisService(IDistributedCache cache, IConfiguration config)
        {
            _cache = cache;
            _config = config;
        }
        public async Task SetRecordAsync<T>(string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
        {
            var opt = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromMinutes(60),
                SlidingExpiration = unusedExpireTime
            };
            var jsonData=System.Text.Json.JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(recordId,jsonData,opt);
        }
        public async Task<T?> GetRecordAsync<T>(string recordId)
        {
            var data=await _cache.GetStringAsync(recordId);
            return data != null ? JsonSerializer.Deserialize<T>(data) : default;
        }

        public async ValueTask<bool> PublishAsync<T>(T record, string channel)
        {
            var jsonData=JsonSerializer.Serialize(record);
            var conString = _config.GetConnectionString("Redis");
            var connection=ConnectionMultiplexer.Connect(conString);
            return await connection.GetSubscriber().PublishAsync(channel, jsonData)>0;
        }

        public async Task<T> SubscribeAsync<T>(string channel)
        {
            T result=default(T);
            var conString = _config.GetConnectionString("Redis");
            var connection = ConnectionMultiplexer.Connect(conString);
            await connection.GetSubscriber().SubscribeAsync(channel,(channel,message)=>
            {
                var res=JsonSerializer.Deserialize<T>(message);
                result = res;
            });
            return result;
        }
    }
}
